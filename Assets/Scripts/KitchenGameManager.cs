using System;
using UnityEngine;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Furniture;

namespace ZZOT.KitchenChaos
{
    public class KitchenGameManager : MonoBehaviour
    {
        public static KitchenGameManager Instance { get; private set; }

        public event EventHandler OnStateChanged;
        public event EventHandler OnGamePaused;
        public event EventHandler OnGameUnpaused;

        public enum GameState
        {
            UNKNOWN = 0,
            WaitingToStart = 10,
            CountdownToStart = 20,
            GamePlaying = 50,
            GameOver = 80,
        }

        private GameState _state;
        [SerializeField] private float _waitingToStartTimer = 1f;
        [SerializeField] private float _countdownToStartTimer = 3f;

        [SerializeField] private float _gamePlayingTimerMax = 30f;
        private float _gamePlayingTimer = 0;
        private bool _isGamePaused = false;


        private uint _succesfulRecipesAmount;

        private void Awake()
        {
            _state = GameState.WaitingToStart;
            Instance = this;
        }

        private void Start()
        {
            DeliveryCounter.Instance.OnRecipeSuccessfulyDelivered += DeliveryCounter_OnRecipeSuccessfulyDelivered;
            UserInput.Instance.OnPauseAction += UserInput_OnPauseAction;
        }

        private void OnDestroy()
        {
            DeliveryCounter.Instance.OnRecipeSuccessfulyDelivered -= DeliveryCounter_OnRecipeSuccessfulyDelivered;
            UserInput.Instance.OnPauseAction -= UserInput_OnPauseAction;
        }

        private void UserInput_OnPauseAction(object sender, EventArgs e)
        {
            TogglePauseGame();
        }

        private void TogglePauseGame()
        {
            _isGamePaused = !_isGamePaused;
            Time.timeScale = _isGamePaused ? 0f : 1f;

            if(_isGamePaused)
            {
                OnGamePaused?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            }
        }

        private void DeliveryCounter_OnRecipeSuccessfulyDelivered(object sender, EventArgs e)
        {
            _succesfulRecipesAmount += 1;
        }

        private void Update()
        {
            switch (_state)
            {
                case GameState.WaitingToStart:
                    _waitingToStartTimer -= Time.deltaTime;
                    if (_waitingToStartTimer < 0)
                    {
                        _state = GameState.CountdownToStart;
                        _gamePlayingTimer = 0;
                        _succesfulRecipesAmount = 0;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case GameState.CountdownToStart:
                    _countdownToStartTimer -= Time.deltaTime;
                    if (_countdownToStartTimer < 0)
                    {
                        _state = GameState.GamePlaying;
                        _gamePlayingTimer = _gamePlayingTimerMax;
                        _succesfulRecipesAmount = 0;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case GameState.GamePlaying:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer < 0)
                    {
                        _state = GameState.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case GameState.GameOver:
                    break;
                default:
                    Debug.LogError("Game State is UNKNOWN.");
                    break;
            }
        }

        public bool IsGamePlaying => _state == GameState.GamePlaying;

        public bool IsCountdownToStartActive => _state == GameState.CountdownToStart;

        public bool IsGameOver => _state == GameState.GameOver;

        public float GetCountdownToStartTimer() => _countdownToStartTimer;

        public float GetPlayingTimerNormalized() =>
            1 - Mathf.Clamp01(_gamePlayingTimer / _gamePlayingTimerMax);

        public uint GetDeliveredRecipesCount() => _succesfulRecipesAmount;

        public void ResumeGame()
        {
            if (_isGamePaused)
            {
                TogglePauseGame();
            }
        }
    }
}
