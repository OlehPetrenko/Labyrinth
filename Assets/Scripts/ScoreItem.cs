using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Stores data of the score.
    /// </summary>
    public class ScoreItem : MonoBehaviour
    {
        private Text _name;
        private Text _score;
        private Text _duration;
        private Text _date;
        private Text _result;

        public string Name
        {
            get { return _name.text; }
            set { _name.text = value; }
        }

        public string Score
        {
            get { return _score.text; }
            set { _score.text = value; }
        }

        public string Duration
        {
            get { return _duration.text; }
            set { _duration.text = value; }
        }

        public string Date
        {
            get { return _date.text; }
            set { _date.text = value; }
        }

        public string Result
        {
            get { return _result.text; }
            set { _result.text = value; }
        }


        private void Awake()
        {
            _name = transform.Find("Name").GetComponent<Text>();
            _score = transform.Find("Score").GetComponent<Text>();
            _duration = transform.Find("Duration").GetComponent<Text>();
            _date = transform.Find("Date").GetComponent<Text>();
            _result = transform.Find("Result").GetComponent<Text>();
        }
    }
}
