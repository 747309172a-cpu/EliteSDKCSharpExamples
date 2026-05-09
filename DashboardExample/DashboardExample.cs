using EliteRobots.CSharp;

internal static class DashboardClientFlowExample
{
    private const string DefaultRobotIp = "172.16.100.10";
    private const int DefaultDashboardPort = 29999;

    public static void Main(string[] args)
    {
        Run(args);
    }

    internal static void Run(string[] args)
    {
        var ip = DefaultRobotIp;
        var port = DefaultDashboardPort;

        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            ip = args[1];
        }

        for (var i = 2; i < args.Length; i++)
        {
            if (args[i] == "--port" && i + 1 < args.Length && int.TryParse(args[++i], out var parsedPort))
            {
                port = parsedPort;
            }
        }

        using var dashboard = new DashboardClientInterface();

        Console.WriteLine($"[INFO] Connecting to dashboard at {ip}:{port}...");
        var connected = dashboard.connect(ip, port);
        Console.WriteLine($"[INFO] Connected: {connected}");

        var version = dashboard.version();
        Console.WriteLine($"[INFO] Dashboard version: {version}");

        const int newScaling = 30;
        Console.WriteLine($"[INFO] Setting speed scaling to {newScaling}%...");
        dashboard.setSpeedScaling(newScaling);

        var currentScaling = dashboard.speedScaling();
        Console.WriteLine($"[INFO] Current speed scaling: {currentScaling}%");

        var mode = dashboard.robotMode();
        var safety = dashboard.safetyMode();
        Console.WriteLine($"[INFO] Robot mode: {mode}");
        Console.WriteLine($"[INFO] Safety mode: {safety}");

        Console.WriteLine("[INFO] Displaying popup message on dashboard...");
        dashboard.popup("-s", "Hello from C# SDK!");

        Console.WriteLine("[INFO] Powering on the robot...");
        dashboard.powerOn();

        Console.WriteLine("[INFO] Releasing robot brakes...");
        dashboard.brakeRelease();

        Console.WriteLine("[INFO] Powering off the robot...");
        dashboard.powerOff();

        Console.WriteLine("[INFO] Disconnecting from dashboard...");
        dashboard.disconnect();
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run -- dashboard_client [robot-ip] [--port <dashboard-port>]");
    }
}

