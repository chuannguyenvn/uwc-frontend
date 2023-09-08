using System;
using System.Collections.Generic;

public static class DataStoreDummy
{
    public static List<(string content, DateTime timestamp, bool isFromUser)> MessageData = new()
    {
        ("Short and sweet.", DateTime.Now.AddMinutes(-19), false),
        ("Hi there!", DateTime.Now.AddMinutes(-18), true),
        ("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vel turpis at ipsum posuere sollicitudin. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vel turpis at ipsum posuere sollicitudin. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vel turpis at ipsum posuere sollicitudin. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vel turpis at ipsum posuere sollicitudin.", DateTime.Now.AddMinutes(-17),
            true),
        ("Quick response. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vel turpis at ipsum posuere sollicitudin. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vel turpis at ipsum posuere sollicitudin.", DateTime.Now.AddMinutes(-14), false),
        ("Good morning!", DateTime.Now.AddMinutes(-10), false),
        ("Happy Friday!", DateTime.Now, true),
        ("This is a test message.", DateTime.Now.AddMinutes(-10), true),
        ("A quick message.", DateTime.Now.AddMinutes(-9), true),
        ("Hey, what's up?", DateTime.Now.AddMinutes(-12), false),
        ("Short message", DateTime.Now.AddMinutes(-2), false),
        ("Testing...", DateTime.Now, false),
        ("Hello, how are you?", DateTime.Now.AddMinutes(-15), true),
        ("Another quick message.", DateTime.Now.AddMinutes(-8), false),
        ("Just a few words.", DateTime.Now.AddMinutes(-5), true),
        ("Another message for testing the UI layout. Let's see how it handles longer messages.", DateTime.Now.AddMinutes(-1), true),
        ("Hello from the other side!", DateTime.Now.AddMinutes(-2), true),
        ("How's the weather today?", DateTime.Now.AddMinutes(-1), false),
        ("Longer message to fill the screen with content for testing purposes.", DateTime.Now.AddMinutes(-7), false),
        ("This is a longer message to test the UI layout. It should span multiple lines when displayed in the UI.", DateTime.Now.AddMinutes(-5),
            true),
    };
}