using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho
{
    public class LoginErrors
    {
        public const int ERR_VER_MISSMATCH = -2;
        public const int ERR_VER_BADPASS = -1;
    }

    public enum Permissions
    {
        None = 0,
        Normal = 1,
        BAT = 2,
        Subscriber = 4
    }
    public enum bStatus
    {
        Idle,
        Afk,
        Playing,
        Editing,
        Modding,
        Multiplayer,
        Watching,
        Unknown,
        Testing,
        Submitting,
        StatsUpdate,
        Paused,
        Lobby
    }
    public enum RequestType
    {
        Osu_SendUserStatus,
        Osu_SendIrcMessage,
        Osu_Exit,
        Osu_RequestStatusUpdate,
        Osu_Pong,
        Bancho_LoginReply,
        Bancho_CommandError,
        Bancho_SendIrcMessage,
        Bancho_Ping,
        Bancho_HandleIrcChangeUsername,
        Bancho_HandleIrcQuit,
        Bancho_HandleIrcJoin,
        Bancho_HandleOsuUpdate,
        Bancho_HandleOsuQuit,
        Bancho_SpectatorJoined,
        Bancho_SpectatorLeft,
        Bancho_SpectateFrames,
        Osu_StartSpectating,
        Osu_StopSpectating,
        Osu_SpectateFrames,
        Bancho_VersionUpdate,
        Osu_ErrorReport,
        Osu_CantSpectate,
        Bancho_SpectatorCantSpectate,
        Bancho_GetAttention,
        Bancho_Announce,
        Osu_SendIrcMessagePrivate,
        Bancho_MatchUpdate,
        Bancho_MatchNew,
        Bancho_MatchDisband,
        Osu_LobbyPart,
        Osu_LobbyJoin,
        Osu_MatchCreate,
        Osu_MatchJoin,
        Osu_MatchPart,
        Bancho_LobbyJoin,
        Bancho_LobbyPart,
        Bancho_MatchJoinSuccess,
        Bancho_MatchJoinFail,
        Osu_MatchChangeSlot,
        Osu_MatchReady,
        Osu_MatchLock,
        Osu_MatchChangeSettings,
        Bancho_FellowSpectatorJoined,
        Bancho_FellowSpectatorLeft,
        Osu_MatchStart,
        AllPlayersLoaded,
        Bancho_MatchStart,
        Osu_MatchScoreUpdate,
        Bancho_MatchScoreUpdate,
        Osu_MatchComplete,
        Bancho_MatchTransferHost,
        Osu_MatchChangeMods,
        Osu_MatchLoadComplete,
        Bancho_MatchAllPlayersLoaded,
        Osu_MatchNoBeatmap,
        Osu_MatchNotReady,
        Osu_MatchFailed,
        Bancho_MatchPlayerFailed,
        Bancho_MatchComplete,
        Osu_MatchHasBeatmap,
        Osu_MatchSkipRequest,
        Bancho_MatchSkip,
        Bancho_Unauthorised,
        Osu_ChannelJoin,
        Bancho_ChannelJoinSuccess,
        Bancho_ChannelAvailable,
        Bancho_ChannelRevoked,
        Bancho_ChannelAvailableAutojoin,
        Osu_BeatmapInfoRequest,
        Bancho_BeatmapInfoReply,
        Osu_MatchTransferHost,
        Bancho_LoginPermissions,
        Bancho_FriendsList,
        Osu_FriendAdd,
        Osu_FriendRemove,
        Bancho_ProtocolNegotiation
    }
    public enum Mods
    {

        None,
        NoFail,
        Easy,
        NoVideo = 4,
        Hidden = 8,
        HardRock = 16,
        SuddenDeath = 32,
        DoubleTime = 64,
        Relax = 128,
        HalfTime = 256,
        Taiko = 512
    }
    public enum Completeness
    {
        StatusOnly,
        Statistics,
        Full
    }
    public enum SlotStatus
    {
        Open = 1,
        Locked = 2,
        NotReady = 4,
        Ready = 8,
        NoMap = 16,
        Playing = 32,
        Complete = 64,
        HasPlayer = 124
    }
    public enum MatchTypes
    {
        Standard,
        Powerplay
    }
}
