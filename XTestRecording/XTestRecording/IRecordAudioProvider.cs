using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTestRecording
{
    public interface IRecordAudioProvider: IDisposable
    {
        void Start(string filename);

        void Stop();

    }
}
