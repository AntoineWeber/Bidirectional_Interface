﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains constant values of the simulation parameters.
/// It contains information about the drone, the room, the mocap system, etc.
/// These constants can be accessed from any script as follows: SimulationData.VariableName
/// </summary>
public static class SimulationData
{
    public const float DroneSize = 0.09f; // [m], roughly square 9x9cm
    public const float DroneWeight = 0.027f; // [kg]

    public const float MocapUpdateDeltaTime = 0.008f; // [s], a new data point every 8ms
    public const int nbParametersMocap = 8; // index, pos (3), quaternion (4)

    public static readonly Vector3 RoomDimensions = new Vector3(8, 6, 8); // [m^3]
}

