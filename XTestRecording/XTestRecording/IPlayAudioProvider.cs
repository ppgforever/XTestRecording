using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTestRecording
{
    public interface IPlayAudioProvider: IDisposable
    {
        event EventHandler PlayAudioCompleted;

        bool IsPlaying { get; }

        Task StartAsync(string filename);

        void Stop();
    }
}
