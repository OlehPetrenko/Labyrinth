using System.Collections.Generic;
using System.Linq;
using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides logic for virtual scroll.
    /// </summary>
    public class VirtualScroll : MonoBehaviour
    {
        [SerializeField] private int _numberOfShownItems;
        [SerializeField] private int _spacing;
        [SerializeField] private int _startPositionY;

        [SerializeField] private GameObject _items;
        [SerializeField] private GameObject _scrollRect;
        [SerializeField] private ScoreItem _scoreItem;

        private List<ScoreItemDto> _scores;
        private List<ScoreItem> _scoreItems;

        private int _index = -1;
        private int _itemHeightWithSpacing;

        private List<ScoreItemDto> Scores
        {
            get
            {
                if (_scores != null)
                    return _scores;

                return _scores = GameSessionData.Instance.Scores.ToList();
            }
        }


        private void Awake()
        {
            _scrollRect.GetComponent<ScrollRect>().onValueChanged.AddListener(ListenerMethod);

            InitializeScores();
        }

        public void ListenerMethod(Vector2 value)
        {
            var currentIndex = (int)_items.transform.localPosition.y / _itemHeightWithSpacing - 1;

            if (_index < currentIndex)
                SetScoreItemBelow();
            if (_index > currentIndex)
                SetScoreItemAbove();

            _index = currentIndex;
        }

        private void InitializeScores()
        {
            _scoreItems = new List<ScoreItem>();

            if (_scoreItem == null || _scoreItem.GetComponent<RectTransform>() == null)
                return;

            _itemHeightWithSpacing = (int)_scoreItem.GetComponent<RectTransform>().rect.height + _spacing;

            //
            // If number of score items less than max number of items on UI
            // create only the right amount of objects.
            //
            if (_numberOfShownItems > Scores.Count)
                _numberOfShownItems = Scores.Count;

            for (var i = 0; i < _numberOfShownItems; i++)
            {
                var scoreItem = Instantiate(_scoreItem, _items.transform);
                var scoreItemRectTransform = scoreItem.GetComponent<RectTransform>();

                var position = _startPositionY - _itemHeightWithSpacing * i;

                scoreItemRectTransform.localPosition = new Vector3(0, position, 0);
                scoreItemRectTransform.localScale = Vector3.one;

                FillScoreItem(scoreItem, Scores[i]);

                _scoreItems.Add(scoreItem);
            }
        }

        public void SetScoreItemBelow()
        {
            var currentIndex = (int)_items.transform.localPosition.y / _itemHeightWithSpacing - 1;

            if (currentIndex + _numberOfShownItems >= Scores.Count)
                return;

            if (currentIndex < 0)
                return;

            var lastItemPosition = GetLastItem().GetComponent<RectTransform>().localPosition.y - _itemHeightWithSpacing;
            var topItem = GetFirstItem();

            var scoreItemDto = Scores[currentIndex + _numberOfShownItems];

            topItem.GetComponent<RectTransform>().localPosition = new Vector3(0, lastItemPosition, 0);

            FillScoreItem(topItem, scoreItemDto);
        }

        public void SetScoreItemAbove()
        {
            var currentIndex = (int)_items.transform.localPosition.y / _itemHeightWithSpacing;

            if (currentIndex < 0 || currentIndex + _numberOfShownItems + 1 > Scores.Count)
                return;

            if (currentIndex < 0)
                return;

            var firstItemPosition = GetFirstItem().GetComponent<RectTransform>().localPosition.y + _itemHeightWithSpacing;
            var bottomItem = GetLastItem();

            var scoreItemDto = Scores[currentIndex];

            bottomItem.GetComponent<RectTransform>().localPosition = new Vector3(0, firstItemPosition, 0);

            FillScoreItem(bottomItem, scoreItemDto);
        }

        private ScoreItem GetFirstItem()
        {
            var maxY = (int)_scoreItems.Max(score => score.GetComponent<RectTransform>().localPosition.y);
            return _scoreItems.First(score => (int)score.GetComponent<RectTransform>().localPosition.y == maxY);
        }

        private ScoreItem GetLastItem()
        {
            var minY = (int)_scoreItems.Min(score => score.GetComponent<RectTransform>().localPosition.y);
            return _scoreItems.First(score => (int)score.GetComponent<RectTransform>().localPosition.y == minY);
        }

        private void FillScoreItem(ScoreItem scoreItem, ScoreItemDto scoreItemDto)
        {
            scoreItem.Name = scoreItemDto.Name;
            scoreItem.Score = scoreItemDto.Score;
            scoreItem.Date = scoreItemDto.Date;
            scoreItem.Duration = scoreItemDto.Duration;
            scoreItem.Result = scoreItemDto.Result;
        }
    }
}
