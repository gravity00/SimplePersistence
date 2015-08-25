using System;
using System.Threading.Tasks;
using SimplePersistence.UoW.Properties;

namespace SimplePersistence.UoW.Helper
{
    internal static class UtilityExtensions
    {
        public static Exception NewDefaultException()
        {
            return new Exception(Resources.TaskInFaultedStateExceptionMessage);
        }

        public static void SetExceptionFromTask<T>(this TaskCompletionSource<T> tcs, Task task)
        {
            if (!task.IsFaulted) return;

            if (task.Exception == null) //  It should never happen
                tcs.SetException(NewDefaultException());
            else
            {
                task.Exception.Flatten().Handle(ex =>
                {
                    tcs.SetException(ex ?? NewDefaultException());
                    return true;
                });
            }
        }
    }
}
