using EliteRobots.CSharp;

internal static class PoseAlgebraExample
{
    private static readonly string DefaultPluginLibPath = Path.Combine(AppContext.BaseDirectory, "elite_eigen_pose_algebra.dll");
    private const string DefaultPluginClassName = "ELITE::EigenPoseAlgebra";

    public static void Main(string[] args)
    {
        Run(args);
    }

    internal static void Run(string[] args)
    {
        if (!TryParseArgs(args, out var pluginLibPath, out var pluginClassName))
        {
            PrintUsage();
            return;
        }

        try
        {
            using var poseAlgebra = new PoseAlgebraBase(pluginLibPath, pluginClassName);

            var basePose = new[] { 0.400000, -0.200000, 0.500000, 0.100000, 0.200000, -0.300000 };
            var toolOffset = new[] { 0.050000, 0.000000, 0.120000, 0.000000, 0.000000, 1.570796 };

            var baseMatrix = default(PoseMatrix);
            if (!CheckResult("vectorToMatrix(base_pose)", poseAlgebra.vectorToMatrix(basePose, ref baseMatrix, out var result), result))
            {
                return;
            }

            var toolMatrix = default(PoseMatrix);
            if (!CheckResult("vectorToMatrix(tool_offset)", poseAlgebra.vectorToMatrix(toolOffset, ref toolMatrix, out result), result))
            {
                return;
            }

            var composedMatrix = default(PoseMatrix);
            if (!CheckResult("multiply(base_matrix, tool_matrix)", poseAlgebra.multiply(baseMatrix, toolMatrix, ref composedMatrix, out result), result))
            {
                return;
            }

            var toolInBaseMatrix = default(PoseMatrix);
            if (!CheckResult("worldToLocal(base_matrix, composed_matrix)", poseAlgebra.worldToLocal(baseMatrix, composedMatrix, ref toolInBaseMatrix, out result), result))
            {
                return;
            }

            var recoveredWorldMatrix = default(PoseMatrix);
            if (!CheckResult("localToWorld(base_matrix, tool_in_base_matrix)", poseAlgebra.localToWorld(baseMatrix, toolInBaseMatrix, ref recoveredWorldMatrix, out result), result))
            {
                return;
            }

            var inverseBaseMatrix = default(PoseMatrix);
            if (!CheckResult("inverse(base_matrix)", poseAlgebra.inverse(baseMatrix, ref inverseBaseMatrix, out result), result))
            {
                return;
            }

            var identityCheck = default(PoseMatrix);
            if (!CheckResult("multiply(base_matrix, inverse_base_matrix)", poseAlgebra.multiply(baseMatrix, inverseBaseMatrix, ref identityCheck, out result), result))
            {
                return;
            }

            var composedPose = new double[6];
            if (!CheckResult("matrixToVector(composed_matrix)", poseAlgebra.matrixToVector(composedMatrix, composedPose, out result), result))
            {
                return;
            }

            var toolInBasePose = new double[6];
            if (!CheckResult("worldToLocal(base_pose, composed_pose)", poseAlgebra.worldToLocal(basePose, composedPose, toolInBasePose, out result), result))
            {
                return;
            }

            var recoveredWorldPose = new double[6];
            if (!CheckResult("localToWorld(base_pose, tool_in_base_pose)", poseAlgebra.localToWorld(basePose, toolInBasePose, recoveredWorldPose, out result), result))
            {
                return;
            }

            var addedPose = new double[6];
            if (!CheckResult("add(base_pose, tool_offset)", poseAlgebra.add(basePose, toolOffset, addedPose, out result), result))
            {
                return;
            }

            var recoveredPose = new double[6];
            if (!CheckResult("subtract(added_pose, tool_offset)", poseAlgebra.subtract(addedPose, toolOffset, recoveredPose, out result), result))
            {
                return;
            }

            if (!CheckResult("distance(base_pose, composed_pose)", poseAlgebra.distance(basePose, composedPose, out var distance, out result), result))
            {
                return;
            }

            Console.WriteLine("=== Pose Algebra Example (Eigen Plugin) ===");
            PrintVector6("base_pose", basePose);
            PrintVector6("tool_offset", toolOffset);
            PrintPoseMatrix("base_matrix", baseMatrix);
            PrintPoseMatrix("composed_matrix", composedMatrix);
            PrintPoseMatrix("tool_in_base_matrix", toolInBaseMatrix);
            PrintPoseMatrix("recovered_world_matrix", recoveredWorldMatrix);
            PrintPoseMatrix("identity_check", identityCheck);
            PrintVector6("composed_pose", composedPose);
            PrintVector6("tool_in_base_pose", toolInBasePose);
            PrintVector6("recovered_world_pose", recoveredWorldPose);
            PrintVector6("added_pose", addedPose);
            PrintVector6("recovered_pose", recoveredPose);
            Console.WriteLine($"distance.linear_distance  = {distance.linear_distance:F6}");
            Console.WriteLine($"distance.angular_distance = {distance.angular_distance:F6}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }

    private static bool TryParseArgs(string[] args, out string pluginLibPath, out string pluginClassName)
    {
        pluginLibPath = DefaultPluginLibPath;
        pluginClassName = DefaultPluginClassName;

        var index = 0;
        if (args.Length > 0 &&
            (string.Equals(args[0], "pose_algebra", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(args[0], "calculation", StringComparison.OrdinalIgnoreCase)))
        {
            index = 1;
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

    private static bool CheckResult(string operation, bool ok, PoseAlgebraResult result)
    {
        if (ok)
        {
            return true;
        }

        Console.WriteLine($"[ERROR] {operation} failed. error={result.error}, message={result.message}");
        return false;
    }

    private static void PrintVector6(string name, double[] value)
    {
        Console.WriteLine($"{name} = [{string.Join(", ", value.Select(v => v.ToString("F6")))}]");
    }

    private static void PrintPoseMatrix(string name, PoseMatrix value)
    {
        Console.WriteLine($"{name} =");
        for (var row = 0; row < 4; row++)
        {
            var cols = Enumerable.Range(0, 4).Select(col => value.data[row * 4 + col].ToString("F6"));
            Console.WriteLine($"  [{string.Join(", ", cols)}]");
        }
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run -- <pose-algebra-plugin-lib> [plugin-class]");
        Console.WriteLine("Or edit DefaultPluginLibPath in PoseAlgebraExample.cs, then run from Visual Studio.");
    }
}
