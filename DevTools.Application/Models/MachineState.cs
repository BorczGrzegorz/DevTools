using Extensions.Infrastructure.Models;
using System;

namespace DevTools.Application.Models
{
    public class MachineState
    {
        public MachineId Id { get; }
        public string UserName { get; }
        public DateTime? LockedDate { get; }

        private MachineState(MachineId machineId, string userName, DateTime? lockedTime)
        {
            Id = machineId;
            UserName = userName;
            LockedDate = lockedTime;
        }

        public static MachineState ForLocked(MachineId machineId, string userName)
        {
            return new MachineState(machineId, userName, DateTime.UtcNow);
        }

        public static MachineState ForUnLocked(MachineId machineId)
        {
            return new MachineState(machineId, null, null);
        }

        internal bool IsLocked() => !string.IsNullOrEmpty(UserName);
    }
}
