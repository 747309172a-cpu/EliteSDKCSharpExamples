[中文](./README_CN.md)

# Elite Robots C# SDK Examples

This repository is a collection of C# sample projects for the Elite Robots CS SDK. It is mainly intended to help developers quickly verify robot connectivity in a Windows environment, understand the basic usage of each communication interface, and run typical robot-control workflows.

This repository already includes the SDK binaries required to run the examples, located in [`libs`](./libs); shared scripts and RTSI recipe files are located in [`resource`](./resource).

## Repository Contents

- `ConnectRobotTest`: verifies whether the robot can actively connect back to the PC TCP server
- `DashboardExample`: basic dashboard connection and control flow
- `EliteDriverExample`: end-to-end `EliteDriver` API usage example
- `KinematicsExample`: FK/IK example using the kinematics plugin
- `PoseAlgebraExample`: pose matrix/vector calculation example using the pose algebra plugin
- `PrimaryPortExample`: primary port connection, package reading, script sending, and exception callback
- `RtsiExample`: RTSI connection, recipe setup, data streaming, and input writeback
- `SerialExample`: tool RS485 communication example based on `EliteDriver`
- `ServojExample`: `writeServoj` motion example based on RTSI feedback and external control
- `SpeedlExample`: speed planning example
- `TrajectoryExample`: trajectory point streaming example using `EliteDriver`, including trajectory feedback and speed/time control modes
- `libs`: local SDK binaries used by all examples
- `resource`: shared scripts and RTSI recipe files
- `doc/API`: SDK API reference in English and Chinese

## Requirements

- .NET SDK 8.0+
- Network access to the target robot
- Please keep the SDK runtime files in the `libs` directory

Recommended controller versions:

- 2.13.x: `>= 2.13.4`
- 2.14.x: `>= 2.14.2`

## API Reference

- English: [doc/API/en/API.en.md](./doc/API/en/API.en.md)
- Chinese: [doc/API/cn/API.cn.md](./doc/API/cn/API.cn.md)

## Changelog

- English: [ChangeLog.md](./ChangeLog.md)
- Chinese: [ChangeLog.cn.md](./ChangeLog.cn.md)
