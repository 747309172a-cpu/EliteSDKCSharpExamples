# PoseAlgebraBase

## Introduction

`PoseAlgebraBase` is the C# wrapper for the pose algebra plugin interface. It provides pose vector/matrix conversion, pose composition, inverse, frame conversion, addition/subtraction, and distance calculation.

## Import

```csharp
using EliteRobots.CSharp;
```

## Constructor

```csharp
public PoseAlgebraBase(string plugin_lib_path, string? plugin_class_name = null)
```

- ***Function***
  - Load the pose algebra plugin and create an instance.
- ***Parameters***
  - `plugin_lib_path`: path to the pose algebra plugin dynamic library.
  - `plugin_class_name`: plugin class name, for example `ELITE::EigenPoseAlgebra`.

## Related Types

### PoseMatrix

```csharp
public struct PoseMatrix
{
    public double[] data;
}
```

- `data` must have length 16 and is stored in row-major order.

### PoseDistance

```csharp
public struct PoseDistance
{
    public double linear_distance;
    public double angular_distance;
}
```

### PoseAlgebraResult

```csharp
public struct PoseAlgebraResult
{
    public PoseAlgebraError error;
    public string message;
}
```

## Methods

### inverse

```csharp
public bool inverse(PoseMatrix pose, ref PoseMatrix inverse_pose, out PoseAlgebraResult result)
public bool inverse(double[] pose, double[] inverse_pose, out PoseAlgebraResult result)
```

- ***Function***
  - Calculate the inverse of a pose matrix or pose vector.

### multiply

```csharp
public bool multiply(PoseMatrix left_pose, PoseMatrix right_pose, ref PoseMatrix out_pose, out PoseAlgebraResult result)
public bool multiply(double[] left_pose, double[] right_pose, double[] out_pose, out PoseAlgebraResult result)
```

- ***Function***
  - Compose two poses.

### add

```csharp
public bool add(PoseMatrix left_pose, PoseMatrix right_pose, ref PoseMatrix out_pose, out PoseAlgebraResult result)
public bool add(double[] left_pose, double[] right_pose, double[] out_pose, out PoseAlgebraResult result)
```

- ***Function***
  - Add two poses.

### subtract

```csharp
public bool subtract(PoseMatrix left_pose, PoseMatrix right_pose, ref PoseMatrix out_pose, out PoseAlgebraResult result)
public bool subtract(double[] left_pose, double[] right_pose, double[] out_pose, out PoseAlgebraResult result)
```

- ***Function***
  - Subtract two poses.

### vectorToMatrix

```csharp
public bool vectorToMatrix(double[] pose_vector, ref PoseMatrix pose_matrix, out PoseAlgebraResult result)
```

- ***Function***
  - Convert a 6D pose vector to a 4x4 pose matrix.

### matrixToVector

```csharp
public bool matrixToVector(PoseMatrix pose_matrix, double[] pose_vector, out PoseAlgebraResult result)
```

- ***Function***
  - Convert a 4x4 pose matrix to a 6D pose vector.

### distance

```csharp
public bool distance(PoseMatrix pose_a, PoseMatrix pose_b, out PoseDistance out_distance, out PoseAlgebraResult result)
public bool distance(double[] pose_a, double[] pose_b, out PoseDistance out_distance, out PoseAlgebraResult result)
```

- ***Function***
  - Calculate linear and angular distance between two poses.

### worldToLocal

```csharp
public bool worldToLocal(PoseMatrix world_ref_pose, PoseMatrix world_pose, ref PoseMatrix local_pose, out PoseAlgebraResult result)
public bool worldToLocal(double[] world_ref_pose, double[] world_pose, double[] local_pose, out PoseAlgebraResult result)
```

- ***Function***
  - Convert a world-frame pose to the local frame of a reference pose.

### localToWorld

```csharp
public bool localToWorld(PoseMatrix world_ref_pose, PoseMatrix local_pose, ref PoseMatrix world_pose, out PoseAlgebraResult result)
public bool localToWorld(double[] world_ref_pose, double[] local_pose, double[] world_pose, out PoseAlgebraResult result)
```

- ***Function***
  - Convert a local-frame pose relative to a reference pose to the world frame.

### Dispose

```csharp
public void Dispose()
```

- ***Function***
  - Release the native pose algebra plugin instance.

## Notes

- All pose vector arrays must have length 6.
- All `PoseMatrix.data` arrays must have length 16.
- If an operation returns `false`, check `PoseAlgebraResult.error` and `PoseAlgebraResult.message`.

## Example

```bash
dotnet run --project PoseAlgebraExample/PoseAlgebraExample.csproj -- /path/to/libelite_eigen_pose_algebra.so
```
