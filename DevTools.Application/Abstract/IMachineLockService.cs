using Extensions.Infrastructure.Models;
using DevTools.Application.Models;
using System.Collections.Generic;

namespace DevTools.Application
{
    public interface IMachineLockService
    {
        MachineState Lock(MachineId machineId, string userName);
        void Release(MachineId machineId, string userName);
        List<MachineState> GetAllStates();
    }
}
