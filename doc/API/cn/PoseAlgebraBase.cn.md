# PoseAlgebraBase

## 简介

`PoseAlgebraBase` 是位姿代数插件接口的 C# 封装。它提供位姿向量/矩阵转换、位姿复合、求逆、坐标系转换、加减和距离计算。

## 导入

```csharp
using EliteRobots.CSharp;
```

## 构造函数

```csharp
public PoseAlgebraBase(string plugin_lib_path, string? plugin_class_name = null)
```

- ***功能***
  - 加载位姿代数插件并创建实例。
- ***参数***
  - `plugin_lib_path`：位姿代数插件动态库路径。
  - `plugin_class_name`：插件类名，例如 `ELITE::EigenPoseAlgebra`。

## 相关类型

### PoseMatrix

```csharp
public struct PoseMatrix
{
    public double[] data;
}
```

- `data` 长度必须为 16，按行优先顺序存储。

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

## 方法

### inverse

```csharp
public bool inverse(PoseMatrix pose, ref PoseMatrix inverse_pose, out PoseAlgebraResult result)
public bool inverse(double[] pose, double[] inverse_pose, out PoseAlgebraResult result)
```

- ***功能***
  - 计算位姿矩阵或位姿向量的逆。

### multiply

```csharp
public bool multiply(PoseMatrix left_pose, PoseMatrix right_pose, ref PoseMatrix out_pose, out PoseAlgebraResult result)
public bool multiply(double[] left_pose, double[] right_pose, double[] out_pose, out PoseAlgebraResult result)
```

- ***功能***
  - 复合两个位姿。

### add

```csharp
public bool add(PoseMatrix left_pose, PoseMatrix right_pose, ref PoseMatrix out_pose, out PoseAlgebraResult result)
public bool add(double[] left_pose, double[] right_pose, double[] out_pose, out PoseAlgebraResult result)
```

- ***功能***
  - 对两个位姿执行加法运算。

### subtract

```csharp
public bool subtract(PoseMatrix left_pose, PoseMatrix right_pose, ref PoseMatrix out_pose, out PoseAlgebraResult result)
public bool subtract(double[] left_pose, double[] right_pose, double[] out_pose, out PoseAlgebraResult result)
```

- ***功能***
  - 对两个位姿执行减法运算。

### vectorToMatrix

```csharp
public bool vectorToMatrix(double[] pose_vector, ref PoseMatrix pose_matrix, out PoseAlgebraResult result)
```

- ***功能***
  - 将 6D 位姿向量转换为 4x4 位姿矩阵。

### matrixToVector

```csharp
public bool matrixToVector(PoseMatrix pose_matrix, double[] pose_vector, out PoseAlgebraResult result)
```

- ***功能***
  - 将 4x4 位姿矩阵转换为 6D 位姿向量。

### distance

```csharp
public bool distance(PoseMatrix pose_a, PoseMatrix pose_b, out PoseDistance out_distance, out PoseAlgebraResult result)
public bool distance(double[] pose_a, double[] pose_b, out PoseDistance out_distance, out PoseAlgebraResult result)
```

- ***功能***
  - 计算两个位姿之间的线性距离和角度距离。

### worldToLocal

```csharp
public bool worldToLocal(PoseMatrix world_ref_pose, PoseMatrix world_pose, ref PoseMatrix local_pose, out PoseAlgebraResult result)
public bool worldToLocal(double[] world_ref_pose, double[] world_pose, double[] local_pose, out PoseAlgebraResult result)
```

- ***功能***
  - 将世界坐标系下的位姿转换到参考位姿的局部坐标系。

### localToWorld

```csharp
public bool localToWorld(PoseMatrix world_ref_pose, PoseMatrix local_pose, ref PoseMatrix world_pose, out PoseAlgebraResult result)
public bool localToWorld(double[] world_ref_pose, double[] local_pose, double[] world_pose, out PoseAlgebraResult result)
```

- ***功能***
  - 将参考位姿局部坐标系下的位姿转换到世界坐标系。

### Dispose

```csharp
public void Dispose()
```

- ***功能***
  - 释放 native 位姿代数插件实例。

## 注意事项

- 所有位姿向量数组长度必须为 6。
- 所有 `PoseMatrix.data` 数组长度必须为 16。
- 当返回 `false` 时，可查看 `PoseAlgebraResult.error` 和 `PoseAlgebraResult.message`。

## 示例

```bash
dotnet run --project PoseAlgebraExample/PoseAlgebraExample.csproj -- /path/to/libelite_eigen_pose_algebra.so
```
