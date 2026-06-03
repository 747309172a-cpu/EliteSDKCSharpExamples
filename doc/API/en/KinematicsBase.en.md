# KinematicsBase

## Introduction

`KinematicsBase` is the C# wrapper for the kinematics plugin interface. It loads a kinematics plugin through the SDK native layer and provides MDH setup, forward kinematics, inverse kinematics, and timeout configuration.

## Import

```csharp
using EliteRobots.CSharp;
```

## Constructor

```csharp
public KinematicsBase(string plugin_lib_path, string? plugin_class_name = null)
```

- ***Function***
  - Load the kinematics plugin and create a solver instance.
- ***Parameters***
  - `plugin_lib_path`: path to the kinematics plugin dynamic library.
  - `plugin_class_name`: plugin class name, for example `ELITE::KdlKinematicsPlugin`.

## Related Types

### KinematicsResult

```csharp
public struct KinematicsResult
{
    public KinematicError kinematic_error;
}
```

### KinematicError

```csharp
public enum KinematicError
{
    OK = 1,
    SOLVER_NOT_ACTIVE = 2,
    NO_SOLUTION = 3,
}
```

## Methods

### setMDH

```csharp
public void setMDH(double[] alpha, double[] a, double[] d)
```

- ***Function***
  - Set the robot MDH parameters.
- ***Parameters***
  - `alpha`, `a`, `d`: MDH arrays. Each array length must be 6.

### getPositionFK

```csharp
public bool getPositionFK(double[] joint_angles, double[] poses)
```

- ***Function***
  - Calculate the TCP pose from joint angles.
- ***Parameters***
  - `joint_angles`: input joint angles, length must be 6.
  - `poses`: output TCP pose, length must be 6.
- ***Return Value***
  - Returns `true` on success, otherwise `false`.

### getPositionIK

```csharp
public bool getPositionIK(double[] pose, double[] near, double[] solution, out KinematicsResult result)
```

- ***Function***
  - Calculate one inverse kinematics solution.
- ***Parameters***
  - `pose`: input TCP pose, length must be 6.
  - `near`: reference joint position, length must be 6.
  - `solution`: output joint solution, length must be 6.
  - `result`: kinematics result.
- ***Return Value***
  - Returns `true` on success, otherwise `false`.

### getPositionIK

```csharp
public bool getPositionIK(double[] pose, double[] near, List<double[]> solutions, out KinematicsResult result)
public bool getPositionIK(double[] pose, double[] near, List<double[]> solutions, int max_solutions, out KinematicsResult result)
```

- ***Function***
  - Calculate multiple inverse kinematics solutions.
- ***Parameters***
  - `pose`: input TCP pose, length must be 6.
  - `near`: reference joint position, length must be 6.
  - `solutions`: output solution list.
  - `max_solutions`: maximum number of solutions to return.
  - `result`: kinematics result.
- ***Return Value***
  - Returns `true` on success, otherwise `false`.

### setDefaultTimeout

```csharp
public void setDefaultTimeout(double timeout)
```

- ***Function***
  - Set the default timeout used by the kinematics solver.

### getDefaultTimeout

```csharp
public double getDefaultTimeout()
```

- ***Function***
  - Get the default timeout used by the kinematics solver.

### Dispose

```csharp
public void Dispose()
```

- ***Function***
  - Release the native kinematics plugin instance.

## Example

```bash
dotnet run --project KinematicsExample/KinematicsExample.csproj -- 172.16.100.10 /path/to/libelite_kdl_kinematics.so
```
