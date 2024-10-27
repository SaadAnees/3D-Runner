using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameData
{
    //public class ApiResponse
    //{
    //    public string status;
    //    public string message { get; set; }
    //}

    //public class LoginResponse : ApiResponse
    //{
    //    public UserInfo details { get; set; }

    //}
    //public class UserInfo
    //{
    //    public string token { get; set; }
    //    public string character { get; set; }
    //    public string username { get; set; }
    //    public string email { get; set; }
    //    public int genderId { get; set; }
    //    public int score { get; set; }
    //    public int checkpoint { get; set; }
    //    public int level { get; set; }
    //    public string quotes { get; set; }
    //    public List<PowerUp> powerups { get; set; }
    //}
    //public class PowerUp
    //{
    //    public string id;
    //    public string powerup;
    //    public string quantity;
    //}

    //public class StoryBoardResponse : ApiResponse
    //{
    //    public List<StoryBoardInfo> details { get; set; }
    //}
    //public class StoryBoardInfo
    //{
    //    public int sequence { get; set; }
    //    public string header { get; set; }
    //    public string body { get; set; }
    //}


    public class Player
    {

        public class Details
        {
            public string access_token;
            public int expires_in;
            public string token_type;
        }


        public class Levels
        {
            public int level;
            public int stage;
            public int check_point;
            public int challengeId;
        }


        public class Mission
        {
            public int required_points;
            public int distance;
            public int time_limit;
        }


        public class GamePlay
        {
            public int total_points;
            public int life;
            public int speed;
            public int powerup;
            public int armor;

        }


        public class GameMessage
        {
            public string level_instruction;
            public string quotes;
        }


        public class UserDetails
        {
            public string name;
            public string email;
            public string gender;
            public int age;
            public string language;
        }


        public class PowerUp
        {
            public string id;
            public string powerup;
            public string quantity;
        }


        public class PlayerState
        {
            public string name;
            public string status;
            public Details details;
            public string message;
            public Levels levels;
            public Mission mission;
            public GamePlay game_play;
            public GameMessage game_message;
            public UserDetails user_details;
            public List<PowerUp> power_up;
            public string character;
            public int stars;
            //public static PlayerState CreateFromJSON(string jsonString)
            //{
            //    return JsonUtility.FromJson<PlayerState>(jsonString);
            //}
        }
    }

    public class StarAPI
    {
        public class Detail
        {
            public string level_id;
            public string stage_id;
            public string checkpoint_id;
            public string star;
        }

        public class Star
        {
            public string status;
            public List<Detail> details;
            public string message;
        }
    }

    public class Story
    {
        public class Detail
        {
            public string id;
            public string sequence;
            public string challenge_setting_id;
            public string body;
            public string header;
            public string last_update;
            public string character_image;
            public string background_image;
            public string video;
        }

        public class Storyboard
        {
            public string status;
            public List<Detail> details;
            public string message;
        }
    }

    public class AwardsAPI
    {
        public class Detail
        {
            public int awardId;
            public string awardName;
            public DateTime awardLastUpdate;
        }

        public class Awards
        {
            public List<Detail> details;
            public string message;
            public string status;
        }
    }

    public class PowerUpAPI
    {
        public class Detail
        {
            public int id;
            public string powerup;
            public int cost;
            public int quantity;
        }

        public class Power
        {
            public List<Detail> details;
            public string message;
            public string status;
        }
    }


    
}
