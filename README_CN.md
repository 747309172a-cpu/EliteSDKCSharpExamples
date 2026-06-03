[English](./README.md)

# Elite Robots C# SDK 示例集

本仓库是 Elite Robots CS SDK 的 C# 示例工程集合，主要用于帮助开发者在windows环境中快速验证机器人连通性、了解各通信接口的基本用法，并运行典型的机器人控制流程。

当前仓库已经内置示例运行所需的 SDK 二进制文件，位于 [`libs`](./libs)；示例共用的脚本和 RTSI 配方文件位于 [`resource`](./resource)。

## 仓库内容

- `ConnectRobotTest`：验证机器人能否主动回连到 PC 端 TCP 服务
- `DashboardExample`：演示 Dashboard 接口的基础连接与控制流程
- `EliteDriverExample`：演示 `EliteDriver` 主要接口的完整调用流程
- `KinematicsExample`：基于运动学插件的 FK/IK 示例
- `PoseAlgebraExample`：基于姿态代数插件的位姿矩阵/向量计算示例
- `PrimaryPortExample`：演示 Primary Port 连接、报文读取、脚本下发和异常回调
- `RtsiExample`：演示 RTSI 连接、配方配置、数据收发与输入写入
- `SerialExample`：基于 `EliteDriver` 的工具端 RS485 通信示例
- `ServojExample`：结合 RTSI 反馈与外部控制脚本的 `writeServoj` 运动示例
- `SpeedlExample`：速度规划示例
- `TrajectoryExample`：基于 `EliteDriver` 的轨迹点下发示例，包含轨迹反馈和速度/时间控制模式
- `libs`：各示例共用的本地 SDK 动态库
- `resource`：各示例共用的脚本和 RTSI 配方文件
- `doc/API`：中英文 API 文档

## 运行要求

- .NET SDK 8.0+
- 可访问目标机器人的网络环境
- 请保留 `libs` 目录中的 SDK 运行库文件

推荐控制器版本：

- 2.13.x：`>= 2.13.4`
- 2.14.x：`>= 2.14.2`



## API 文档

- 中文：[doc/API/cn/API.cn.md](./doc/API/cn/API.cn.md)
- English: [doc/API/en/API.en.md](./doc/API/en/API.en.md)

## 更新记录

- 中文：[ChangeLog.cn.md](./ChangeLog.cn.md)
- English: [ChangeLog.md](./ChangeLog.md)
