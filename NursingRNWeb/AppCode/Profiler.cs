using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// Helper class that wraps the timer based functionalities.
/// </summary>
public class Profiler
{
    /// <summary>
    /// Lock object.
    /// </summary>
    /// <summary>
    /// Variable that tracks the time.
    /// </summary>
    private readonly Dictionary<string, Stopwatch> ProfilePool;

    /// <summary>
    /// Initializes static members of the <see cref="Profiler"/> class. 
    /// </summary>
    public Profiler()
    {
        ProfilePool = new Dictionary<string, Stopwatch>();
    }

    /// <summary>
    /// Starts this timer.
    /// </summary>
    public string Start(string methodName)
    {
        var newMethodName = methodName;
        var name = ProfilePool.Where(pair => pair.Key.IndexOf(methodName) != -1)
                .OrderByDescending(pair => pair.Key.Length)
                .FirstOrDefault().Key;

        if (name != null)
        {
            newMethodName = name + "1";
        }

        var stopwatch = new Stopwatch();
        ProfilePool.Add(newMethodName, stopwatch);
        stopwatch.Start();
        return newMethodName;
    }

    /// <summary>
    /// Stops timer and calculate the execution time.
    /// </summary>
    /// <returns>Execution time in milli seconds</returns>
    public double Stop(string methodName)
    {
        double elaspsedTime = 0;
        if (ProfilePool.ContainsKey(methodName))
        {
            var stopwatch = ProfilePool[methodName];
            stopwatch.Stop();
            elaspsedTime = stopwatch.Elapsed.TotalMilliseconds;
            ProfilePool.Remove(methodName);
        }

        return elaspsedTime;
    }
}