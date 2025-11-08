using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Texts // texts: intro, riddles, correct answers
    {
        public readonly string intro0 =  // intro
            "\n| I open my eyes and feel a strong headache. I close my eyes again and realize that " +
            "\n| the difference in sensations is none. With incredible effort, I sit up in bed. " +
            "\n| I lean over and rest my head on my hands. I sit like this for a while" +
            "\n| and try to understand at least something, to remember. Where am I? " +
            "\n| Why do I feel so bad? Who am I? The last question especially scares me. " +
            "\n| Gradually, the veil fades from my eyes. And I have the opportunity to look around " +
            "\n| the room where I found myself. A room without windows, a table and a chair," +
            "\n| white walls around, covered with cushions, a bright lamp on the ceiling" +
            "\n| 'as soon as I looked at it, the pain in my head pierced me like a lightning bolt'. " +
            "\n| After a while, I notice a bottle on the table and feel a strong thirst. " +
            "\n| I gather myself and try to reach it. When I drank it completely, I almost immediately" +
            "\n| felt the pain ease. Only then do I notice that there was a note under the bottle...";

        public readonly string intro1 =  // note from "god" 1/2
            "\n1/2\n" +
            "\n| How do you feel? Your head should be crushed, but that usually happens after " +
            "\n| taking the elixir of amnesia (I swear I warned you). The water should help a bit," +
            "\n| of course, it’s not ordinary water. And of course, it wasn't in the order," +
            "\n| just a gift from me. I have to admit, you have rather unusual requests," +
            "\n| but you pay enough for me not to ask unnecessary questions." +
            "\n| Well, let's get to the point...";

        public readonly string intro2 =  // note from "god" 2/2
            "\n2/2\n" +
            "\n| This room, as you may have noticed, is unusual, take a look around" +
            "\n| - everything you need for life is here, except for communication," +
            "\n| unless you count the \"talking wall\" of course." +
            "\n| Yes, soon it will annoy you to play with it." +
            "\n| You do want to know what awaits you beyond this impenetrable metal door," +
            "\n| don't you? To save time and health, I'll tell you right away that" +
            "\n| you can't open it other than by playing. That means your only chance" +
            "\n| is to play with the wall and win. If you manage to pass all the tests," +
            "\n| find me on the surface (look for a player with the nickname \"god\"). Good luck!" +
            "\n\nPS: You asked me to remind you of your name if the \"wall\" can't do it...";


        public string[] Riddles = new string[10]  // array with 10 riddles
        {
        "\n 1 | I am without sound, without measurement, without color. I fill the whole world, but I am invisible..." +
        "\n-----------------------------------------------------------------------------------------",
        "\n 2 | It stands still, but also doesn't. You can't catch it, but you still lose it..." +
        "\n------------------------------------------------------------------------------------",
        "\n 3 | What animal walks on four legs in the morning, on two legs in the afternoon, and on three legs in the evening?" +
        "\n--------------------------------------------------------------------------------------",
        "\n 4 | Always goes forward, but never moves..." +
        "\n----------------------------------------------------",
        "\n 5 | Grows when you feed it, but dies when you give it a drink..." +
        "\n---------------------------------------------------------------",
        "\n 6 | Something that can never be filled, but always remains..." +
        "\n---------------------------------------------------------------------",
        "\n 7 | It was invented by the one who didn't want it. For the one who bought it, it is unnecessary. " +
        "\n The one who needs it is silent..." +
        "\n---------------------------------------------------------------------------------------------------------------",
        "\n 8 | I have never been. I am always awaited. No one has seen me and never will." +
        "\n And yet everyone who lives and breathes relies on me. Who am I?" +
        "\n-----------------------------------------------------------------------------",
        "\n 9 | You can't see it, hold it in your hand, hear it with your ear, or smell it with your nose. " +
        "\n It rules the heavens, hides in every pit. It was at the beginning and will be after everything." +
        "\n Every life ends and it kills laughter..." +
        "\n----------------------------------------------------------------------------",
        "\n 10 | What gets bigger when you take away from it?" +
        "\n------------------------------------------------"
        };

        public string[] Answers = new string[10]  // answers to the riddles
        {
        "air",
        "breath",
        "human",
        "time",
        "fire",
        "emptiness",
        "coffin",
        "tomorrow",
        "darkness",
        "hole"
        };
    }
}
