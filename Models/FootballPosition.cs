using System.Text.Json.Serialization;
namespace FootballMgm.Api.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FootballPosition
{
    Goalkeeper,     // GK
    RightBack,      // RB
    LeftBack,       // LB
    CenterBack,     // CB
    RightWingBack,  // RWB
    LeftWingBack,   // LWB
    DefensiveMidfielder, // CDM
    CentralMidfielder,   // CM
    AttackingMidfielder, // CAM
    RightWinger,    // RW
    LeftWinger,     // LW
    Forward,        // CF or ST (Center Forward / Striker)
}