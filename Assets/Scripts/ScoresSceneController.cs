using System;
using System.Collections.Generic;
using Assets.Classes;
using Mono.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoresSceneController : MonoBehaviour
    {
        private ScoreItem _scoreItem;

        private List<ScoreItem> _scoreItems;

        private GameObject _items;

        private void Awake()
        {
            _scoreItem = Resources.Load("ScoreItem", typeof(ScoreItem)) as ScoreItem;

            _scoreItems = new List<ScoreItem>();

            _items = GameObject.Find("Items");


            var aa = new List<ScoreItemDto>();

            for (int i = 0; i < 15; i++)
            {
                var temp = new ScoreItemDto
                {
                    Name = "Oleh" + i,
                    Score = "111",
                    Date = DateTime.Today.ToShortDateString(),
                    Duration = "00:00:01",
                    Result = "lol"
                };

                aa.Add(temp);
            }

            UpdateScores(aa);

            ScrollToTop();
        }

        public void ScrollToTop()
        {
            GameObject.Find("ScrollRect").GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
            //scrollRect.normalizedPosition = new Vector2(0, 1);
        }

        private void UpdateScores(List<ScoreItemDto> scoreItems)
        {
            for (int i = 0; i < 7; i++)
            {
                var scoreItem = Instantiate(_scoreItem);

                scoreItem.transform.parent = _items.transform;

                scoreItem.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                scoreItem.GetComponent<RectTransform>().localScale = Vector3.one;

                scoreItem.Name = scoreItems[i].Name;
                scoreItem.Score = scoreItems[i].Score;
                scoreItem.Date = scoreItems[i].Date;
                scoreItem.Duration = scoreItems[i].Duration;
                scoreItem.Result = scoreItems[i].Result;
            }
        }



        public void OpenMainMenu()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
