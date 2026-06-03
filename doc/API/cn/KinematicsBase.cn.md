# KinematicsBase

## 简介

`KinematicsBase` 是运动学插件接口的 C# 封装。它通过 SDK native 层加载运动学插件，并提供 MDH 参数设置、正运动学、逆运动学和超时时间配置。

## 导入

```csharp
using EliteRobots.CSharp;
```

## 构造函数

```csharp
public KinematicsBase(string plugin_lib_path, string? plugin_class_name = null)
```

- ***功能***
  - 加载运动学插件并创建求解器实例。
- ***参数***
  - `plugin_lib_path`：运动学插件动态库路径。
  - `plugin_class_name`：插件类名，例如 `ELITE::KdlKinematicsPlugin`。

## 相关类型

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

## 方法

### setMDH

```csharp
public void setMDH(double[] alpha, double[] a, double[] d)
```

- ***功能***
  - 设置机器人 MDH 参数。
- ***参数***
  - `alpha`、`a`、`d`：MDH 参数数组，每个数组长度必须为 6。

### getPositionFK

```csharp
public bool getPositionFK(double[] joint_angles, double[] poses)
```

- ***功能***
  - 根据关节角计算 TCP 位姿。
- ***参数***
  - `joint_angles`：输入关节角，长度必须为 6。
  - `poses`：输出 TCP 位姿，长度必须为 6。
- ***返回值***
  - 成功返回 `true`，失败返回 `false`。

### getPositionIK

```csharp
public bool getPositionIK(double[] pose, double[] near, double[] solution, out KinematicsResult result)
```

- ***功能***
  - 计算一组逆运动学解。
- ***参数***
  - `pose`：输入 TCP 位姿，长度必须为 6。
  - `near`：参考关节角，长度必须为 6。
  - `solution`：输出关节角解，长度必须为 6。
  - `result`：运动学求解结果。
- ***返回值***
  - 成功返回 `true`，失败返回 `false`。

### getPositionIK

```csharp
public bool getPositionIK(double[] pose, double[] near, List<double[]> solutions, out KinematicsResult result)
public bool getPositionIK(double[] pose, double[] near, List<double[]> solutions, int max_solutions, out KinematicsResult result)
```

- ***功能***
  - 计算多组逆运动学解。
- ***参数***
  - `pose`：输入 TCP 位姿，长度必须为 6。
  - `near`：参考关节角，长度必须为 6。
  - `solutions`：输出解列表。
  - `max_solutions`：最多返回的解数量。
  - `result`：运动学求解结果。
- ***返回值***
  - 成功返回 `true`，失败返回 `false`。

### setDefaultTimeout

```csharp
public void setDefaultTimeout(double timeout)
```

- ***功能***
  - 设置运动学求解器默认超时时间。

### getDefaultTimeout

```csharp
public double getDefaultTimeout()
```

- ***功能***
  - 获取运动学求解器默认超时时间。

### Dispose

```csharp
public void Dispose()
```

- ***功能***
  - 释放 native 运动学插件实例。

## 示例

```bash
dotnet run --project KinematicsExample/KinematicsExample.csproj -- 172.16.100.10 /path/to/libelite_kdl_kinematics.so
```
