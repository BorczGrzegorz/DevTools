using Extensions.Infrastructure.Models;
using DevTools.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTools.Application
{
    public class MachineLockService : IMachineLockService
    {
        private readonly object _lock = new object();
        private Dictionary<MachineId, MachineState> _stateDictionary;

        public MachineLockService()
        {
            _stateDictionary = new Dictionary<MachineId, MachineState>();
        }

        public MachineState Lock(MachineId machineId, string userName)
        {
            lock (_lock)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentException($"{nameof(userName)} is null or empty!");
                }

                if (!_stateDictionary.ContainsKey(machineId))
                {
                    Add(machineId);
                }

                MachineState state = _stateDictionary[machineId];
                if (state.IsLocked())
                {
                    throw new InvalidOperationException($"machine {machineId} has already ben locked!");
                }

                MachineState newState = MachineState.ForLocked(machineId, userName);
                _stateDictionary[machineId] = newState;
                return newState;
            }
        }

        public void Release(MachineId machineId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException($"{nameof(userName)} is null or empty!");
            }

            lock (_lock)
            {
                if (!_stateDictionary.ContainsKey(machineId))
                {
                    return;
                }

                MachineState state = _stateDictionary[machineId];
                if (state.UserName != userName)
                {
                    throw new ArgumentException("Only owner can release machine!");
                }

                _stateDictionary.Remove(machineId);
            }
        }

        public List<MachineState> GetAllStates()
        {
            return _stateDictionary.Values.ToList();
        }

        private void Add(MachineId machineId)
        {
            _stateDictionary.Add(machineId, MachineState.ForUnLocked(machineId));
        }
    }
}
