using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LabyrinthState
{
    private static float _checkPoint1Amount;
    public static float checkPoint1Amount
    {
        get => _checkPoint1Amount;
        set
        {
            // Debug.Log("1");
            if (_checkPoint1Amount != value)
            {
                // Debug.Log("2");
                _checkPoint1Amount = value;
                NotifyListeners();
            }
        }
    }
    private static bool _checkPoint1Passed;
    public static bool checkPoint1Passed
    {
        get => _checkPoint1Passed;
        set
        {
            _checkPoint1Passed = value;
            NotifyListeners();
        }
    }
    private static float _checkPoint2Amount;

    private static bool _checkPoint2Activated;
    public static bool checkPoint2Activated
    {
        get => _checkPoint2Activated;
        set
        {
            _checkPoint2Activated = value;
            NotifyListeners();
        }
    }
    public static float checkPoint2Amount
    {
        get => _checkPoint2Amount;
        set
        {
            // Debug.Log("1");
            if (_checkPoint2Amount != value)
            {
                // Debug.Log("2");
                _checkPoint2Amount = value;
                NotifyListeners();
            }
        }
    }
    private static bool _checkPoint2Passed;
    public static bool checkPoint2Passed
    {
        get => _checkPoint2Passed;
        set
        {
            _checkPoint2Passed = value;
            NotifyListeners();
        }
    }

    public static bool firstPersonView;
    public static bool isDay;

    private static float _musicVolume;
    public static float musicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            NotifyListeners();
        }
    }
    private static float _effectsVolume;
    public static float effectsVolume
    {
        get => _effectsVolume;
        set
        {
            _effectsVolume = value;
            NotifyListeners();
        }
    }
    private static bool _isSoundMuted;
    public static bool isSoundMuted
    {
        get => _isSoundMuted;
        set
        {
            _isSoundMuted = value;
            NotifyListeners();
        }
    }
    public static bool isPaused;

    public static event Action<string> observers;
    public static Dictionary<string, Action> propertyObservers = new();


    public static void AddListener(Action<string> action)
    {
        if (observers is null)
        {
            observers = new(action);
            return;
        }
        observers += action;
    }

    public static void AddListener(string property, Action action)
    {
        if (!propertyObservers.ContainsKey(property))
        {
            propertyObservers.Add(property, new(action));
            return;
        }

        // Debug.Log($"Before '{property}' added: {propertyObservers[property].GetInvocationList().Length}");
        propertyObservers[property] += action;
        // Debug.Log($"After '{property}' added: {propertyObservers[property].GetInvocationList().Length}");
    }

    public static void RemoveListener(Action<string> action)
    {
        if (observers is null)
        {
            return;
        }
        observers -= action;
    }

    public static void RemoveListener(string property, Action action)
    {
        if (!propertyObservers.ContainsKey(property))
        {
            return;
        }

        foreach (var cb in propertyObservers[property].GetInvocationList())
        {
            propertyObservers[property] -= (Action)cb;
        }
        // Debug.Log($"Before '{property}' removed: {propertyObservers[property].GetInvocationList().Length}");
        // propertyObservers[property] -= action;
        // Debug.Log($"After '{property}' removed: {propertyObservers[property].GetInvocationList().Length}");
    }

    private static void NotifyListeners([CallerMemberName] string propertyName = "")
    {
        if (observers is not null)
        {
            observers.Invoke(propertyName);
        }

        if (propertyObservers.ContainsKey(propertyName))
        {
            propertyObservers[propertyName]?.Invoke();
        }
    }
}
