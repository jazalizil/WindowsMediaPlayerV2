
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using WindowsMediaPlayerV2Core;

namespace WindowsMediaPlayerV2.ViewModel
{
    public class MediaPlayerVM : MVVM.ViewModel
    {
        private Program WMP;
        public String FileName { get; set; }
        public String FileExt { get; set; }
        public String FilePath { get; set; }

        private Dictionary<String, Type> ExtToType;

        public MediaPlayerVM()
        {
            WMP = new Program();
            ExtToType = new Dictionary<string, Type>();
            ExtToType["mp3"] = typeof(WindowsMediaPlayerV2Core.Sound);
            ExtToType["wav"] = typeof(WindowsMediaPlayerV2Core.Sound);
            ExtToType["mp4"] = typeof(WindowsMediaPlayerV2Core.Video);
            ExtToType["avi"] = typeof(WindowsMediaPlayerV2Core.Video);
            ExtToType["jpeg"] = typeof(WindowsMediaPlayerV2Core.Photo);
            ExtToType["jpg"] = typeof(WindowsMediaPlayerV2Core.Photo);
            ExtToType["png"] = typeof(WindowsMediaPlayerV2Core.Photo);
        }

        public ICommand PlayCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    MethodInfo PlayMeth = WMP.player.GetType().GetMethod("Play").MakeGenericMethod();
                    PlayMeth.Invoke(WMP.player, null);
                });
            }
        }
        public ICommand PauseCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                    {
                        MethodInfo PauseMeth = WMP.player.GetType().GetMethod("Pause").MakeGenericMethod();
                        PauseMeth.Invoke(WMP.player, null);
                    });
            }
        }
        public ICommand StopCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                    {
                        MethodInfo StopMeth = WMP.player.GetType().GetMethod("Stop").MakeGenericMethod();
                        StopMeth.Invoke(WMP.player, null);
                    });
            }
        }
        public ICommand CreateCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    MethodInfo meth = WMP.player.GetType().GetMethod("Create");
                    MessageBox.Show(ExtToType[FileExt].ToString());
                    MethodInfo MediaCreate = meth.MakeGenericMethod(ExtToType[FileExt]);
                    var myMedia = (Media)MediaCreate.Invoke(WMP.player, null);
                    myMedia.FileName = FileName;
                    myMedia.Path = FilePath;
                    myMedia.Extension = FileExt;
                });
            }
        }
        public IEnumerable<Media> Playlist
        {
            get { return WMP.player.Playlist; }
        }
    }
}