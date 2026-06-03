using EliteRobots.CSharp;

internal static class KinematicsExample
{
    private const string DefaultRobotIp = "172.16.100.10";
    private const string DefaultPluginLibPath = @"C:\EliteSDK\plugins\kinematics\elite_kdl_kinematics.dll";
    private const string DefaultPluginClassName = "ELITE::KdlKinematicsPlugin";

    public static void Main(string[] args)
    {
        Run(args);
    }

    internal static void Run(string[] args)
    {
        if (!TryParseArgs(args, out var robotIp, out var pluginLibPath, out var pluginClassName))
        {
            PrintUsage();
            return;
        }

        try
        {
            using var primary = new PrimaryClientInterface();
            if (!primary.connect(robotIp, 30001))
            {
                Console.WriteLine("[FATAL] Connect robot 30001 port fail.");
                return;
            }

            if (!primary.getPackage(out var kinInfo, 200))
            {
                Console.WriteLine("[FATAL] Get robot kinematics info fail.");
                return;
            }
            primary.disconnect();
            Console.WriteLine("[INFO] Got robot kinematics info.");

            using var io = new RtsiIoInterface(
                new[] { "actual_joint_positions", "actual_TCP_pose" },
                Array.Empty<string>(),
                250.0);
            if (!io.connect(robotIp))
            {
                Console.WriteLine("[FATAL] Connect robot RTSI port fail.");
                return;
            }

            var currentJoint = io.getActualJointPositions();
            Console.WriteLine("[INFO] Got robot actual joint positions.");

            var currentTcp = io.getActualTCPPose();
            Console.WriteLine("[INFO] Got robot actual TCP positions.");

            using var kinSolver = new KinematicsBase(pluginLibPath, pluginClassName);
            kinSolver.setMDH(kinInfo.DhAlpha, kinInfo.DhA, kinInfo.DhD);

            var fkPose = new double[6];
            if (!kinSolver.getPositionFK(currentJoint, fkPose))
            {
                Console.WriteLine("[FATAL] Get FK fail.");
                return;
            }

            var ikJoints = new double[6];
            if (!kinSolver.getPositionIK(currentTcp, currentJoint, ikJoints, out var ikResult))
            {
                Console.WriteLine($"[FATAL] Get IK fail. error={ikResult.kinematic_error}");
                return;
            }

            PrintVector6("Current TCP Pose", currentTcp);
            PrintVector6("FK Pose", fkPose);
            PrintVector6("IK Result Joints", ikJoints);
            PrintVector6("Current Joints", currentJoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }

    private static bool TryParseArgs(string[] args, out string robotIp, out string pluginLibPath, out string pluginClassName)
    {
        robotIp = DefaultRobotIp;
        pluginLibPath = DefaultPluginLibPath;
        pluginClassName = DefaultPluginClassName;

        var index = 0;
        if (args.Length > 0 && string.Equals(args[0], "kinematics", StringComparison.OrdinalIgnoreCase))
        {
            index = 1;
        }

        if (args.Length > index && !string.IsNullOrWhiteSpace(args[index]))
        {
            robotIp = args[index++];
        }

        if (args.Length > index && !string.IsNullOrWhiteSpace(args[index]))
        {
            pluginLibPath = args[index++];
        }

        if (args.Length > index && !string.IsNullOrWhiteSpace(args[index]))
        {
            pluginClassName = args[index];
        }

        return true;
    }

    private static void PrintVector6(string name, double[] value)
    {
        Console.WriteLine($"{name}: [{string.Join(", ", value.Select(v => v.ToString("F6")))}]");
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run -- [robot-ip] <kinematics-plugin-lib> [plugin-class]");
        Console.WriteLine("Or edit DefaultRobotIp and DefaultPluginLibPath in KinematicsExample.cs, then run from Visual Studio.");
    }
}
