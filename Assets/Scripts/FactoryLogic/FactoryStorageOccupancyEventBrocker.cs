using System;

namespace FactoryLogic
{
    public static class FactoryStorageOccupancyEventBrocker
    {
        public static event Action<Factory> OnEntranceStorageEmpty;
        public static event Action<Factory> OnEntranceStorageNotEmpty;
        public static event Action<Factory> OnExitStorageFull;
        public static event Action<Factory> OnExitStorageNotFull;
        
        
        public static void InvokeEntranceStorageEmpty(Factory obj)
        {
            OnEntranceStorageEmpty?.Invoke(obj);
        }

        public static void InvokeEntranceStorageNotEmpty(Factory obj)
        {
            OnEntranceStorageNotEmpty?.Invoke(obj);
        }
        
        public static void InvokeExitStorageFull(Factory obj)
        {
            OnExitStorageFull?.Invoke(obj);
        }
        
        public static void InvokeExitStorageNotFull(Factory obj)
        {
            OnExitStorageNotFull?.Invoke(obj);
        }
    }
}
