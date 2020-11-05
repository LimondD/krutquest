using System;

namespace KrutQuest.Service.Transport
{
    public class TeamStatus
    {
        public int Score { get; set; }

        public int Timer { get; set; }

        public string Code { get; set; }

        public const string Initial = "initial";
        public const string Started = "started";
        public const string Finished = "finished";
    }
}
