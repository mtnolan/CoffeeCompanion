using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerLib
{
  public class SlackRequest
  {
    private const string TOKEN = "token";
    private const string TEAM_ID = "team_id";
    private const string TEAM_DOMAIN = "team_domain";
    private const string CHANNEL_ID = "channel_id";
    private const string CHANNEL_NAME = "channel_name";
    private const string USER_ID = "user_id";
    private const string USER_NAME = "user_name";
    private const string COMMAND = "command";
    private const string TEXT = "text";
    private const string RESPONSE_URL = "response_url";
    private const string TRIGGER_ID = "trigger_id";


    public SlackRequest(string body)
    {
      var pairs = body.Split('&');

      foreach (var pair in pairs)
      {
        var keyValue = pair.Split('=');
        if (keyValue.Length == 2)
        {
          var key = keyValue[0];
          var value = keyValue[1];

          switch (key)
          {
            case TOKEN:
              Token = value;
              break;

            case TEAM_ID:
              TeamId = value;
              break;

            case TEAM_DOMAIN:
              TeamDomain = value;
              break;

            case CHANNEL_ID:
              ChannelId = value;
              break;

            case CHANNEL_NAME:
              ChannelName = value;
              break;

            case USER_ID:
              UserId = value;
              break;

            case USER_NAME:
              UserName = value;
              break;

            case COMMAND:
              Command = value;
              break;

            case TEXT:
              Text = value;
              break;

            case RESPONSE_URL:
              ResponseUrl = value;
              break;

            case TRIGGER_ID:
              TriggerId = value;
              break;
          }
        }
      }
    }

    public string Token { get; set; }
    public string TeamId { get; set; }
    public string TeamDomain { get; set; }
    public string ChannelId { get; set; }
    public string ChannelName { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Command { get; set; }
    public string Text { get; set; }
    public string ResponseUrl { get; set; }
    public string TriggerId { get; set; }
  }
}
