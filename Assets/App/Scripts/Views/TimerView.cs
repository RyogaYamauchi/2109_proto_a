using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using App.Lib;

namespace App.Views
{
    public class TimerView : PhotonViewBase
    {
        [SerializeField] private Text countText;
        [SerializeField] private Text timerText;
        
        private readonly Subject<Unit> _changeSceneSubject = new Subject<Unit>();

        public IObservable<Unit> ChangeSceneAsObservable => _changeSceneSubject;
        
        private bool _isChange = false;
        public void ChangeCountDown(int countDown)
        {
            countText.text = countDown.ToString();
        }

        public void ChangeTimer(int timer)
        {
            var intTimer = timer;
            timerText.text = intTimer.ToString();
        }
        
        //ルームプロパティが変更されたときに呼ばれる
        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.TryGetValue("scene_state", out var temp))
            {
                if (_isChange) return;
                if ((string)temp == SceneState.SceneStateType.Result.ToString())
                {
                    _isChange = true;
                    _changeSceneSubject.OnNext(Unit.Default);
                }
            }
        }

    }
}

