﻿
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsMediaPlayerV2Core;

namespace WindowsMediaPlayerV2.ViewModel
{
    public class MediaPlayerVM : MVVM.ViewModel
    {
        private Program WMP;
        private String _fPath { get; set; }
        private String _fExt { get; set; }
        private String _fName { get; set; }
        private Dictionary<String, Type> ExtToType;
        private Media _toPlay;

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

        public String FilePath
        {
            get { return _fPath; }
            set
            {
                _fPath = value;
                int index = _fPath.LastIndexOf('\\');
                String fNameWithExt = FilePath.Substring(index + 1);
                index = fNameWithExt.IndexOf('.');
                _fName = fNameWithExt.Substring(0, index);
                _fExt = fNameWithExt.Substring(index + 1);
            }
        }
        public Media ToPlay
        {
            get { return _toPlay; }
            set
            {
                _toPlay = value;
                RaisePropertyChangedEvent("ToPlay");
            }
        }
        public ICommand PlayCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    MethodInfo PlayMeth = WMP.player.GetType().GetMethod("Play");
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
                        MethodInfo PauseMeth = WMP.player.GetType().GetMethod("Pause");
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
                        MethodInfo StopMeth = WMP.player.GetType().GetMethod("Stop");
                        StopMeth.Invoke(WMP.player, null);

                    });
            }
        }
        public ICommand CreateMediaCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    MethodInfo meth = WMP.player.GetType().GetMethod("Create");
                    MethodInfo MediaCreate = meth.MakeGenericMethod(ExtToType[_fExt]);
                    _toPlay = (Media)MediaCreate.Invoke(WMP.player, new String[] { _fPath, _fName, _fExt });
                    RaisePropertyChangedEvent("ToPlay");
                });
            }
        }
        public IEnumerable<Media> Playlist
        {
            get { return WMP.player.Playlist; }
        }
    }
}