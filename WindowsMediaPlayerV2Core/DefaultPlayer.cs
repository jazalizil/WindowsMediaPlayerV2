using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WindowsMediaPlayerV2Core
{
    [Export(typeof(IPlayer))]
    class DefaultPlayer : Player
    {
        MediaElement MediaEl;
        public DefaultPlayer() {
            MediaEl = new MediaElement();
        }
        public override Media Create<T>()
        {
            base.Create<T>();
            //MediaEl.Source = System.Uri(_current.Path);
            return _current;
        }
        public override void Play()
        {
            MediaEl.Play();
        }

        public override void Pause()
        {
            MediaEl.Pause();
        }
        public override void Stop()
        {
            MediaEl.Stop();
        }
        public override void Dispose()
        {
            MediaEl.Stop();
        }
        

    }
}
