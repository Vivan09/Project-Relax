using UnityEngine;

namespace SoftTetris
{
    public class DetectSwipe
    {
        private Board Board { get; set; }

        private Vector2 _startPosition;
        private float _startTime;

        private readonly float _minDistance;
        private readonly float _maxTime;
        
        public DetectSwipe(Board board, float minDistance, float maxTime)
        {
            Board = board;
            _minDistance = minDistance;
            _maxTime = maxTime;
        }

        public void SubscribeEvents()
        {
            Board.OnStartTouch += SwipeStart;
            Board.OnEndTouch += SwipeEnd;
        }

        public void UnsubscribeEvents()
        {
            Board.OnStartTouch -= SwipeStart;
            Board.OnEndTouch -= SwipeEnd;
        }

        void SwipeStart(Vector2 position, float time)
        {
            _startPosition = position;
            _startTime = time;
        }

        void SwipeEnd(Vector2 position, float time) => Detect(position, time);

        void Detect(Vector2 endPosition, float endTime)
        {
            if (Vector3.Distance(_startPosition, endPosition) >= _minDistance && endTime - _startTime <= _maxTime)
            {
                var direction = endPosition - _startPosition;
                Debug.DrawLine(_startPosition, endPosition, Color.black, 1f);
                Board.SwipeDirection(new Vector2(direction.x, direction.y).normalized);
            }
        }
    }
}