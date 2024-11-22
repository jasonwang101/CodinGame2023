using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Score points by scanning valuable fish faster than your opponent.
 **/
class Player
{
    private const bool slience = true;
    private const int MinBorder = 0;
    private const int MaxBorder = 9999;
    private const int NearEdge = 2000;
    private const int Edging = 800;
    private const int SafeZone = 2500;
    private const int TotalFish = 12;

    private static string[] songs = new[] { @"There once was a ship that put to sea
The name of the ship was the Billy O' Tea
The winds blew up her bow dipped down
Oh blow my bully boys blow (huh)
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go
She'd not been two weeks from shore
When down on her a right whale bore
The captain called all hands and swore
He'd take that whale in tow (huh)
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go
Da da da da da
Da da da da da da da
Da da da da da da da da da da da
Before the boat had hit the water
The whale's tail came up and caught her
All hands to the side harpooned and fought her
When she dived down low (huh)
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go
No line was cut no whale was freed
The captain's mind was not of greed
And he belonged to the Whaleman's creed
She took that ship in tow (huh)
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go
Da da da da da
Da da da da da da da
Da da da da da da da da da da da
For forty days or even more
The line went slack then tight once more
All boats were lost there were only four
But still that whale did go (huh)
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go
As far as I've heard the fight's still on
The line's not cut and the whale's not gone
The Wellerman makes his regular call
To encourage the captain crew and all (huh)
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go
Soon may the Wellerman come
To bring us sugar and tea and rum
One day when the tonguing is done
We'll take our leave and go",
@"I don't want a lot for Christmas
There is just one thing I need
I don't care about the presents underneath the Christmas tree
I just want you for my own
More than you could ever know
Make my wish come true
All I want for Christmas is you
Yeah
I don't want a lot for Christmas
There is just one thing I need
Don't care about the presents underneath the Christmas tree
I don't need to hang my stocking there upon the fireplace
Santa Claus won't make me happy with a toy on Christmas Day
I just want you for my own
More than you could ever know
Make my wish come true
All I want for Christmas is you
You baby
Oh I won't ask for much this Christmas
I won't even wish for snow
I'm just gonna keep on waiting underneath the mistletoe
I won't make a list and send it to the North Pole for Saint Nick
I won't even stay awake to hear those magic reindeer click
'Cause I just want you here tonight
Holding on to me so tight
What more can I do?
Oh baby all I want for Christmas is you
You baby
Oh-oh all the lights are shining so brightly everywhere
And the sound of children's laughter fills the air
And everyone is singing
I hear those sleigh bells ringing
Santa won't you bring me the one I really need?
Won't you please bring my baby to me?
Oh I don't want a lot for Christmas
This is all I'm asking for
I just wanna see my baby standing right outside my door
Oh I just want you for my own
More than you could ever know
Make my wish come true
Oh baby all I want for Christmas is you
You baby
All I want for Christmas is you baby
All I want for Christmas is you baby
All I want for Christmas is you baby
All I want for Christmas is you baby
All I want for Christmas is you baby",
@"We're no strangers to love
You know the rules and so do I
A full commitment's what I'm thinking of
You wouldn't get this from any other guy
I just wanna tell you how I'm feeling
Gotta make you understand
Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you
We've known each other for so long
Your heart's been aching but you're too shy to say it
Inside we both know what's been going on
We know the game and we're gonna play it
And if you ask me how I'm feeling
Don't tell me you're too blind to see
Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you
Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you
We've known each other for so long
Your heart's been aching but you're too shy to say it
Inside we both know what's been going on
We know the game and we're gonna play it
I just wanna tell you how I'm feeling
Gotta make you understand
Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you
Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you
Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you"
};

    private static List<string> _words;
    private static Dictionary<int, HiddenCreature> S_TotalCeatures;

    static void Main(string[] args)
    {
        Random random = new Random();
        _words = new List<string>(songs[random.NextInt64(songs.Length)].Replace("\n", " ").Split(' '));
        string[] inputs;
        int creatureCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < creatureCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int creatureId = int.Parse(inputs[0]);
            int color = int.Parse(inputs[1]);
            int type = int.Parse(inputs[2]);
        }

        Dictionary<int, Controller> controllers = new Dictionary<int, Controller>();
        Dictionary<int, Creature> monsters = new Dictionary<int, Creature>();
        Dictionary<int, int> fishes = new Dictionary<int, int>();

        // game loop
        while (true)
        {
            int myScore = int.Parse(Console.ReadLine());
            int foeScore = int.Parse(Console.ReadLine());
            int myScanCount = int.Parse(Console.ReadLine());
            Dictionary<int, int> myCount = GetScan(myScanCount, fishes);
            int foeScanCount = int.Parse(Console.ReadLine());
            Dictionary<int, int> foeCount = GetScan(foeScanCount, fishes);
            int myDroneCount = int.Parse(Console.ReadLine());
            Dictionary<int, Drone> myDrones = GetDrone(myDroneCount, myCount, controllers);
            int foeDroneCount = int.Parse(Console.ReadLine());
            GetFoeDrone(foeDroneCount, foeCount, myDrones);
            int droneScanCount = int.Parse(Console.ReadLine());
            GetCurrentScan(droneScanCount, myDrones, out Dictionary<int, int> currentScan, out Dictionary<int, int> scansPerDrone);
            int visibleCreatureCount = int.Parse(Console.ReadLine());
            GetVisibleCreature(visibleCreatureCount, myDrones, myCount, currentScan, monsters, fishes);
            int radarBlipCount = int.Parse(Console.ReadLine());
            GetHiddenCreature(radarBlipCount, myDrones, myCount, currentScan, monsters, fishes);
            for (int i = 0; i < myDroneCount; i++)
            {
                if (!controllers.TryGetValue(i, out Controller controller))
                {
                    controller = new Controller(myDrones, i, monsters);
                    controllers.Add(i, controller);
                }
                controller.SetData(myScore, foeScore, myCount, foeCount, myDrones, currentScan, scansPerDrone);
                Action action = controller.GetAction();
                controller.LightWasOn[i] = action.Light == 1;
                if (controller.SawMonsterWaitCount < 4)
                {
                    action.Light = 0;
                }
                controller.SawMonsterWaitCount++;
                action.DoAction();

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
            }
        }
    }

    private static void GetCurrentScan(int count, Dictionary<int, Drone> myDrones, out Dictionary<int, int> currentScan, out Dictionary<int, int> scansPerDrone)
    {
        currentScan = new Dictionary<int, int>();
        scansPerDrone = new Dictionary<int, int>();
        for (int i = 0; i < count; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int droneId = int.Parse(inputs[0]);
            int creatureId = int.Parse(inputs[1]);
            if (myDrones.ContainsKey(droneId))
            {
                currentScan.TryAdd(creatureId, droneId);
                if (scansPerDrone.ContainsKey(droneId))
                {
                    scansPerDrone[droneId]++;
                }
                else
                {
                    scansPerDrone.Add(droneId, 1);
                }
            }
        }
    }

    private class Controller
    {
        private const int SaveSurfaceY = 450;
        private const int ResetSawEnoughY = 500;

        public int SawMonsterWaitCount { get; set; } = 0;
        public Dictionary<int, bool> LightWasOn { get; set; }

        private int MyScore { get; set; }
        private int FoeScore { get; set; }
        private Dictionary<int, int> MyScan { get; set; }
        private Dictionary<int, int> FoeScan { get; set; }
        private Dictionary<int, Drone> MyDrones { get; set; }
        private Dictionary<int, Creature> Monsters { get; set; }
        private Dictionary<int, int> CurrentScan { get; set; }
        private Dictionary<int, int> ScansPerDrone { get; set; }

        private int currentOrderStep = 0;
        private int iSawEnough = 0;
        private int changeSideCount = 0;
        private int thisDroneCount;
        private int _count;
        private int _rapidLightCount;
        private bool reset;
        private bool sawMonster;
        private bool reverse;
        private bool isFirstDive;
        private bool _firstDiveGoingUp;
        private bool _goSave;
        private bool changeSide;
        private Radar _firstDiveRadar = Radar.None;
        private List<Radar> moveOrder;

        public Controller(Dictionary<int, Drone> myDrones, int droneCount, Dictionary<int, Creature> monsters)
        {
            thisDroneCount = droneCount;
            Monsters = monsters;
            Drone drone = myDrones.ElementAt(thisDroneCount).Value;
            reverse = !drone.IsLeftSide();
            LightWasOn = new Dictionary<int, bool>()
            {
                { droneCount, false }
            };
            isFirstDive = true;
            _count = 0;
            _rapidLightCount = 0;
        }

        public void SetData(int myScore, int foeScore,
            Dictionary<int, int> myCount,
            Dictionary<int, int> foeCount,
            Dictionary<int, Drone> myDrones,
            Dictionary<int, int> currentScan,
            Dictionary<int, int> scansPerDrone)
        {
            MyScore = myScore;
            FoeScore = foeScore;
            MyScan = myCount;
            FoeScan = foeCount;
            MyDrones = myDrones;
            CurrentScan = currentScan;
            ScansPerDrone = scansPerDrone;

            CreateMoveOrder(reverse);
        }

        public Action GetAction()
        {
            if (_count > _words.Count)
            {
                _count = 0;
            }

            if (_rapidLightCount >= 3)
            {
                _rapidLightCount = 0;
            }

            Action action = null;
            Drone drone = MyDrones.ElementAt(thisDroneCount).Value;
            drone.Count = _count;
            drone.RapidLightCount = _rapidLightCount;
            _count++;
            _rapidLightCount++;

            ChangeSide(drone, out action);
            if (action != null)
            {
                return action;
            }

            LookForDanger(drone, out action);
            if (action != null)
            {
                return action;
            }

            FirsDive(drone, out action);
            if (action != null)
            {
                return action;
            }

            GoSave(drone, out action);
            if (action != null)
            {
                return action;
            }

            return SearchForFishes(drone);
        }

        private void ChangeSide(Drone drone, out Action action)
        {
            action = null;

            if (changeSideCount > 3)
            {
                changeSide = false;
            }

            if (changeSide && drone.Y < SafeZone - 500)
            {
                drone.NextX = drone.X + (drone.IsLeftSide() ? Drone.MaxDistancePerTurn : -Drone.MaxDistancePerTurn);
                drone.NextY = drone.Y;
                drone.OverrideMessage = "Not going this way.";
                action = drone.GetAction();
                changeSideCount++;
            }
        }

        private void FirsDive(Drone drone, out Action action)
        {
            action = null;

            if (_firstDiveGoingUp && drone.Y <= SaveSurfaceY)
            {
                changeSideCount = 0;
                changeSide = true;
                isFirstDive = false;
                _firstDiveGoingUp = false;
            }

            if (!isFirstDive)
            {
                return;
            }

            if (sawMonster ||
                (drone.RadarCount[Radar.BL].Count == 0 && drone.RadarCount[Radar.BR].Count == 0) ||
                (MyScan.Count + CurrentScan.Count >= TotalFish))
            {
                _firstDiveGoingUp = true;
            }

            if (_firstDiveGoingUp)
            {
                drone.LoadMoveXY(Radar.Up);
                drone.OverrideLight = 0;
                drone.OverriddenLight = true;
                action = drone.GetAction();
                return;
            }

            Radar radar = Radar.Down;
            if (_firstDiveRadar != Radar.None && drone.RadarCount[_firstDiveRadar].Count > 0)
            {
                drone.LoadMoveXY(_firstDiveRadar);
                drone.OverrideLight = 1;
                drone.OverriddenLight = true;
                action = drone.GetAction();
                return;
            }

            if (!reverse)
            {
                radar = Radar.BR;
                if (drone.RadarCount[Radar.BL].Count + 2 >= drone.RadarCount[Radar.BR].Count)
                {
                    radar = _firstDiveRadar = Radar.BL;
                }
            }
            else
            {
                radar = Radar.BL;
                if (drone.RadarCount[Radar.BR].Count + 2 >= drone.RadarCount[Radar.BL].Count)
                {
                    radar = _firstDiveRadar = Radar.BR;
                }
            }

            drone.LoadMoveXY(radar);
            drone.OverrideLight = 1;
            drone.OverriddenLight = true;
            action = drone.GetAction();
            return;
        }

        private void PersonalSpace(Drone drone, out Action action)
        {
            double x = 0.0;
            double y = 0.0;

            bool inRange = false;

            action = null;
            foreach (Drone enemy in drone.FoeDrone.Values)
            {
                Console.Error.WriteLine($"Drone {drone.X} {drone.Y}");
                Console.Error.WriteLine($"Enemy {enemy.X} {enemy.Y}");

                x += drone.X - enemy.X;
                y += drone.Y - enemy.Y;

                inRange = true;
            }

            if (inRange)
            {
                action = GetActionFromXY(drone, x, y, false, "PERSONAL SPACE!");
            }
        }

        private void GoSave(Drone drone, out Action action)
        {
            action = null;
            if (MyScan.Count + CurrentScan.Count >= TotalFish || drone.RadarHiddenCreatures.Count == 0 || _goSave)
            {
                drone.OverrideMessage = "We got everything!";
                drone.LoadMoveXY(Radar.None);
                action = drone.GetAction();
            }

            if (ScansPerDrone.TryGetValue(drone.DroneId, out int count))
            {
                if ((iSawEnough >= 3 && count > 2) || (drone.Y > 8500 && count > 4) || (drone.Y < SafeZone && count > 0))
                {
                    drone.OverrideMessage = "I got some. This is fine.";
                    if (drone.RadarHiddenCreatures.TryGetValue(reverse ? Radar.TR : Radar.TL, out HiddenCreature creature) ||
                        drone.RadarHiddenCreatures.TryGetValue(reverse ? Radar.TL : Radar.TR, out creature))
                    {
                        drone.LoadMoveXY(creature.Radar);
                        action = drone.GetAction();
                        return;
                    }
                    drone.LoadMoveXY(Radar.None);
                    action = drone.GetAction();
                }
            }
        }

        private void LookForDanger(Drone drone, out Action action)
        {
            const int tooClose = 500;

            action = null;
            bool flashTheMonster = false;
            bool isTooClose = false;
            bool isAngry = false;

            double x = 0.0;
            double y = 0.0;

            if (drone.VisibleMonsters.Count <= 0)
            {
                return;
            }

            foreach (MonsterVisibleCreature creature in drone.VisibleMonsters.Values)
            {
                if (!creature.IsNotMoving)
                {
                    flashTheMonster = true;
                }

                if (creature.IsAngry)
                {
                    isAngry = true;
                }

                if (drone.InRange(creature, tooClose))
                {
                    isTooClose = true;
                }

                x += drone.X - creature.X;
                y += drone.Y - creature.Y;
            }

            if (drone.Y <= SafeZone && isAngry)
            {
                changeSideCount = 0;
                changeSide = true;
            }

            SawMonsterWaitCount = 0;
            sawMonster = true;
            iSawEnough++;

            if (CheckEdge(drone) || isTooClose)
            {
                action = FindAWayOut(drone, x, y, flashTheMonster);
                return;
            }
            action = GetActionFromXY(drone, x, y, flashTheMonster, "Hehe. I am in danger!");
        }

        private Action FindAWayOut(Drone drone, double x, double y, bool light)
        {
            drone.OverrideMessage = "I am in a pickle.";
            drone.OverrideLight = light ? 1 : 0;
            drone.OverriddenLight = light;
            SawMonsterWaitCount = 0;
            ScaleXY(x, y, out int xToTravel, out int yToTravel);
            bool isLeftGood = MinBorder + Edging < drone.X + xToTravel && x < 200;
            bool isRightGood = MaxBorder - Edging > drone.X + xToTravel && x > -200;
            bool isTopGood = MinBorder + Edging < drone.Y + yToTravel && y < 200;
            bool isBottomGood = MaxBorder - Edging > drone.Y + yToTravel && y > -200;

            Console.Error.WriteLine($"Drone {drone.DroneId} {drone.X} {drone.Y} going to {x} {y} {xToTravel} {yToTravel}");
            Console.Error.WriteLine($"Left {isLeftGood} Right {isRightGood} Top {isTopGood} Bottom {isBottomGood}");

            if (isTopGood && SafeZone + Edging + 100 > drone.Y)
            {
                drone.LoadMoveXY(Radar.Up);
                drone.OverrideMessage = "Can't touch this.";
                changeSideCount = 0;
                changeSide = true;
                return drone.GetAction();
            }

            if (Math.Abs(x) > Math.Abs(y))
            {
                if ((!isBottomGood && !isTopGood && !isLeftGood) ||
                    (!isBottomGood && !isTopGood))
                {
                    drone.OverrideMessage += "Right!";
                    drone.LoadMoveXY(Radar.Right);
                    return drone.GetAction();
                }
                else if ((!isBottomGood && !isTopGood && !isRightGood) ||
                    (!isBottomGood && !isTopGood))
                {
                    drone.OverrideMessage += "Left!";
                    drone.LoadMoveXY(Radar.Left);
                    return drone.GetAction();
                }

                if (x > 0 && isRightGood)
                {
                    drone.OverrideMessage += "Right!";
                    drone.LoadMoveXY(Radar.Right);
                    return drone.GetAction();
                }
                else if (x < 0 && isLeftGood)
                {
                    drone.OverrideMessage += "Left!";
                    drone.LoadMoveXY(Radar.Left);
                    return drone.GetAction();
                }
            }
            else
            {
                if ((!isLeftGood && !isRightGood && !isTopGood) ||
                    (!isLeftGood && !isRightGood))
                {
                    drone.OverrideMessage += "Down!";
                    drone.LoadMoveXY(Radar.Down);
                    return drone.GetAction();
                }
                else if ((!isLeftGood && !isRightGood && !isBottomGood) ||
                    (!isLeftGood && !isRightGood))
                {
                    drone.OverrideMessage += "Up!";
                    drone.LoadMoveXY(Radar.Up);
                    return drone.GetAction();
                }

                if (y > 0 && isBottomGood)
                {
                    drone.LoadMoveXY(Radar.Down);
                    drone.OverrideMessage += "Down!";
                    return drone.GetAction();
                }
                else if (y < 0 && isTopGood)
                {
                    drone.OverrideMessage += "Up!";
                    drone.LoadMoveXY(Radar.Up);
                    return drone.GetAction();
                }
            }

            if (isTopGood)
            {
                drone.OverrideMessage += "Up!";
                drone.LoadMoveXY(Radar.Up);
                return drone.GetAction();
            }

            if (isLeftGood)
            {
                drone.OverrideMessage += "Left!";
                drone.LoadMoveXY(Radar.Left);
                return drone.GetAction();
            }

            if (isRightGood)
            {
                drone.OverrideMessage += "Right!";
                drone.LoadMoveXY(Radar.Right);
                return drone.GetAction();
            }

            if (isBottomGood)
            {
                drone.OverrideMessage += "Down!";
                drone.LoadMoveXY(Radar.Down);
                return drone.GetAction();
            }

            drone.OverrideMessage = "HAIL MARY!";
            drone.NextX = drone.X + xToTravel;
            drone.NextY = drone.Y + yToTravel;
            return drone.GetAction();
        }

        private Action GetActionFromXY(Drone drone, double x, double y, bool light, string message)
        {
            ScaleXY(x, y, out int xToTravel, out int yToTravel);
            drone.NextX = drone.X + xToTravel;
            drone.NextY = drone.Y + yToTravel;
            drone.OverrideLight = light ? 1 : 0;
            drone.OverriddenLight = light;
            drone.OverrideMessage ??= message;
            return drone.GetAction();
        }

        private bool CheckEdge(Drone drone)
        {
            return MinBorder + Edging > drone.X ||
                MaxBorder - Edging < drone.X ||
                MinBorder + Edging > drone.Y ||
                MaxBorder - Edging < drone.Y;
        }

        private void ScaleXY(double x, double y, out int xToTravel, out int yToTravel)
        {
            bool xBigger = Math.Abs(x) > Math.Abs(y);
            double percentage = xBigger ? GetPercentage(Math.Abs(x), Math.Abs(y)) : GetPercentage(Math.Abs(y), Math.Abs(x));
            int distance = (int)(Drone.MaxDistancePerTurn * Math.Abs(percentage));
            if (xBigger)
            {
                yToTravel = Drone.MaxDistancePerTurn - distance;
                xToTravel = Drone.MaxDistancePerTurn - yToTravel;
            }
            else
            {
                xToTravel = Drone.MaxDistancePerTurn - distance;
                yToTravel = Drone.MaxDistancePerTurn - xToTravel;
            }
            if (x < 0)
            {
                xToTravel = -xToTravel;
            }
            if (y < 0)
            {
                yToTravel = -yToTravel;
            }
        }

        private double GetPercentage(double x, double y)
        {
            return x / (x + y);
        }

        private HiddenCreature PickNextCreature(Drone drone)
        {
            HiddenCreature creature = null;
            Console.Error.WriteLine($"Drone {drone.DroneId} Current Step {currentOrderStep}");

            if (drone.RadarHiddenCreatures.Count == 1)
            {
                return drone.RadarHiddenCreatures.First().Value;
            }

            if (drone.Y <= NearEdge)
            {
                sawMonster = false;
                iSawEnough = 0;
                reset = false;
                if (drone.RadarHiddenCreatures.TryGetValue(moveOrder[1], out creature))
                {
                    return creature;
                }
            }

            if (drone.Emergency == 1)
            {
                reset = true;
            }

            if (drone.Emergency == 0 && reset)
            {
                currentOrderStep = 1;
                reset = false;
            }

            if (currentOrderStep >= moveOrder.Count)
            {
                currentOrderStep = 1;
            }

            Radar radar = moveOrder[currentOrderStep];
            if (drone.Y <= SaveSurfaceY)
            {
                currentOrderStep = 1;
                return PickNextCreature(drone);
            }

            if (currentOrderStep > 0)
            {
                Radar previousRadar = moveOrder[currentOrderStep - 1];
                if (drone.RadarHiddenCreatures.TryGetValue(previousRadar, out creature))
                {
                    return creature;
                }
            }

            if (drone.RadarHiddenCreatures.TryGetValue(radar, out creature))
            {
                return creature;
            }
            else
            {
                currentOrderStep++;
                return PickNextCreature(drone);
            }
        }

        private Action SearchForFishes(Drone drone)
        {
            HiddenCreature nextCreature = PickNextCreature(drone);
            Radar radar = nextCreature != null ? nextCreature.Radar : Radar.None;
            drone.LoadMoveXY(radar);
            return drone.GetAction();
        }

        private void CreateMoveOrder(bool reverse)
        {
            if (moveOrder != null && moveOrder.Count > 0)
            {
                return;
            }

            if (reverse)
            {
                moveOrder = new List<Radar> {
                    Radar.TR,
                    Radar.BR,
                    Radar.BL,
                    Radar.TL,
                    Radar.TR
                };
            }
            else
            {
                moveOrder = new List<Radar> {
                    Radar.TL,
                    Radar.BL,
                    Radar.BR,
                    Radar.TR,
                    Radar.TL
                };
            }
        }
    }

    private static void GetVisibleCreature(int count,
        Dictionary<int, Drone> myDrones, Dictionary<int, int> myScan, Dictionary<int, int> currentScan,
        Dictionary<int, Creature> monsters, Dictionary<int, int> fishs)
    {
        const int monsterSpeed = 550;

        for (int i = 0; i < count; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int creatureId = int.Parse(inputs[0]);
            int creatureX = int.Parse(inputs[1]);
            int creatureY = int.Parse(inputs[2]);
            int creatureVx = int.Parse(inputs[3]);
            int creatureVy = int.Parse(inputs[4]);
            VisibleCreature creature = new VisibleCreature(creatureId, creatureX, creatureY, creatureVx, creatureVy);
            foreach (Drone drone in myDrones.Values)
            {
                if (drone.InRange(creature))
                {
                    int totalSpeed = Math.Abs(creatureVx) + Math.Abs(creatureVy);
                    if ((!currentScan.ContainsKey(creatureId) && !myScan.ContainsKey(creatureId)) || monsters.ContainsKey(creatureId))
                    {
                        bool isAngary = totalSpeed > monsterSpeed;
                        MonsterVisibleCreature monster = new MonsterVisibleCreature(creature, isAngary);
                        monsters.TryAdd(creatureId, monster);
                        drone.VisibleMonsters.TryAdd(creatureId, monster);
                        fishs.Remove(creatureId);
                        monster.Print();
                    }
                    else
                    {
                        fishs.TryAdd(creatureId, 0);
                        drone.VisibleCreatures.TryAdd(creatureId, creature);
                        creature.Print();
                    }
                }
                Console.Error.WriteLine($"See fish! {creature.CreatureId}");
            }
        }
    }

    private static void GetHiddenCreature(int count,
        Dictionary<int, Drone> drones,
        Dictionary<int, int> myCount,
        Dictionary<int, int> currentScan,
        Dictionary<int, Creature> monsters,
        Dictionary<int, int> fishes
    )
    {
        List<HiddenCreature> hiddenCreatures = new List<HiddenCreature>();
        Dictionary<int, HiddenCreature> existingCreatures = new Dictionary<int, HiddenCreature>();
        for (int i = 0; i < count; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int droneId = int.Parse(inputs[0]);
            int creatureId = int.Parse(inputs[1]);
            string radar = inputs[2];
            Radar radarEnum = (Radar)Enum.Parse(typeof(Radar), radar);
            Drone drone = drones[droneId];
            HiddenCreature hiddenCreature = new HiddenCreature(creatureId, drone, radarEnum);
            existingCreatures.TryAdd(hiddenCreature.CreatureId, hiddenCreature);
            if (!myCount.ContainsKey(hiddenCreature.CreatureId) && !currentScan.ContainsKey(hiddenCreature.CreatureId) && !monsters.ContainsKey(hiddenCreature.CreatureId))
            {
                hiddenCreatures.Add(hiddenCreature);
            }
        }

        if (S_TotalCeatures == null)
        {
            S_TotalCeatures = existingCreatures;
        }
        else
        {
            foreach (HiddenCreature creature in S_TotalCeatures.Values)
            {
                if (!existingCreatures.ContainsKey(creature.CreatureId))
                {
                    fishes.TryAdd(creature.CreatureId, 0);
                }
            }
        }

        int monsterCount = S_TotalCeatures.Count - TotalFish;
        int missingFishCount = S_TotalCeatures.Count - hiddenCreatures.Count;
        if (myCount.Count + currentScan.Count + missingFishCount + monsterCount == S_TotalCeatures.Count)
        {
            foreach (HiddenCreature hiddenCreature in hiddenCreatures)
            {
                if (!fishes.ContainsKey(hiddenCreature.CreatureId))
                {
                    monsters.TryAdd(hiddenCreature.CreatureId, creature);
                }
            }
            hiddenCreatures.Clear();
        }

        foreach (HiddenCreature hiddenCreature in hiddenCreatures)
        {
            hiddenCreature.Drone.AddHiddenCreature(hiddenCreature);
        }
    }

    private static Dictionary<int, Drone> GetDrone(int count, Dictionary<int, int> scanCount, Dictionary<int, Controller> controllers)
    {
        Dictionary<int, Drone> drones = new Dictionary<int, Drone>();
        for (int i = 0; i < count; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int droneId = int.Parse(inputs[0]);
            int droneX = int.Parse(inputs[1]);
            int droneY = int.Parse(inputs[2]);
            int emergency = int.Parse(inputs[3]);
            int battery = int.Parse(inputs[4]);
            bool light = false;
            if (controllers.ContainsKey(i))
            {
                light = controllers[i].LightWasOn[i];
            }
            drones.Add(droneId, new Drone(droneId, droneX, droneY, emergency, battery, scanCount, light));
        }
        return drones;
    }

    private static void GetFoeDrone(int count, Dictionary<int, int> scanCount, Dictionary<int, Drone> myDrones)
    {
        for (int i = 0; i < count; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int droneId = int.Parse(inputs[0]);
            int droneX = int.Parse(inputs[1]);
            int droneY = int.Parse(inputs[2]);
            int emergency = int.Parse(inputs[3]);
            int battery = int.Parse(inputs[4]);
            Drone foeDrone = new Drone(droneId, droneX, droneY, emergency, battery, scanCount, false);
            foreach (Drone drone in myDrones.Values)
            {
                if (drone.InRange(foeDrone))
                {
                    drone.AddFoeDrone(drone);
                }
            }
        }
    }

    private static Dictionary<int, int> GetScan(int count, Dictionary<int, int> fishes)
    {
        Dictionary<int, int> scan = new Dictionary<int, int>();
        for (int i = 0; i < count; i++)
        {
            int creatureId = int.Parse(Console.ReadLine());
            scan.TryAdd(creatureId, 1);
            fishes.TryAdd(creatureId, 0);
        }
        return scan;
    }

    private enum Radar
    {
        None,
        TL,
        TR,
        BR,
        BL,
        Up,
        Down,
        Left,
        Right
    }

    private class MapCreature
    {
        private Creature Left { get; set; }
        private Creature Right { get; set; }
        private Creature Creature { get; set; }
        private Dictionary<int, Creature> AllCreaturesLeft { get; set; }
        private Dictionary<int, Creature> AllCreaturesRight { get; set; }
    }

    private class Drone
    {
        private const int SaveSurfaceY = 450;
        private const int LightOnDepth = 3000;

        public const int MaxDistancePerTurn = 1000;

        public int DroneId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? NextX { get; set; }
        public int? NextY { get; set; }
        public int Emergency { get; set; }
        public int Battery { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int OverrideLight { get; set; }
        public int Count { get; set; }
        public int RapidLightCount { get; set; }
        public bool OverriddenLight { get; set; }
        public string OverrideMessage { get; set; }
        public Dictionary<int, int> ScanCount { get; set; }
        public Dictionary<int, HiddenCreature> HiddenCreature { get; set; }
        public Dictionary<Radar, HiddenCreature> RadarHiddenCreatures { get; set; }
        public Dictionary<Radar, List<HiddenCreature>> RadarCount { get; set; }
        public Dictionary<int, VisibleCreature> VisibleCreatures { get; set; }
        public Dictionary<int, MonsterVisibleCreature> VisibleMonsters { get; set; }
        public Dictionary<int, Drone> FoeDrone { get; set; }

        private string message;
        private bool LightWasOn;
        private Radar _radar;

        public Drone(int droneId, int x, int y, int emergency, int battery, Dictionary<int, int> scanCount, bool lightWasOn)
        {
            DroneId = droneId;
            X = x;
            Y = y;
            Emergency = emergency;
            Battery = battery;
            ScanCount = scanCount;
            HiddenCreature = new Dictionary<int, HiddenCreature>();
            RadarHiddenCreatures = new Dictionary<Radar, HiddenCreature>();
            RadarCount = new Dictionary<Radar, List<HiddenCreature>>(){
                {Radar.TL, new List<HiddenCreature>()},
                {Radar.TR, new List<HiddenCreature>()},
                {Radar.BL, new List<HiddenCreature>()},
                {Radar.BR, new List<HiddenCreature>()}
            };
            VisibleCreatures = new Dictionary<int, VisibleCreature>();
            VisibleMonsters = new Dictionary<int, MonsterVisibleCreature>();
            FoeDrone = new Dictionary<int, Drone>();
            LightWasOn = lightWasOn;
        }

        public bool IsLeftSide()
        {
            return X < MaxBorder / 2;
        }

        public void AddHiddenCreature(HiddenCreature hiddenCreature)
        {
            HiddenCreature.TryAdd(hiddenCreature.CreatureId, hiddenCreature);
            RadarHiddenCreatures.TryAdd(hiddenCreature.Radar, hiddenCreature);
            RadarCount[hiddenCreature.Radar].Add(hiddenCreature);
        }

        public void AddFoeDrone(Drone drone)
        {
            FoeDrone.TryAdd(drone.DroneId, drone);
        }

        public Action GetAction()
        {
            if (!string.IsNullOrWhiteSpace(OverrideMessage))
            {
                message = OverrideMessage;
            }
            return new Action(NextX, NextY, ToggleLight(), message, _radar, Count);
        }

        public void LoadMoveXY(Radar radar)
        {
            _radar = radar;
            double leftCreatures = (RadarCount[Radar.BL].Count + RadarCount[Radar.TL].Count) / 2.0;
            double rightCreatures = (RadarCount[Radar.BR].Count + RadarCount[Radar.TR].Count) / 2.0;
            double topCreatures = (RadarCount[Radar.TL].Count + RadarCount[Radar.TR].Count) / 2.0;
            double bottomCreatures = (RadarCount[Radar.BL].Count + RadarCount[Radar.BR].Count) / 2.0;

            MinX = leftCreatures == 0 ? X : 0;
            MinY = topCreatures == 0 ? Y : 0;
            MaxX = rightCreatures == 0 ? X : MaxBorder;
            MaxY = bottomCreatures == 0 ? Y : MaxBorder;

            switch (radar)
            {
                case Radar.TL:
                    MoveTL();
                    break;
                case Radar.TR:
                    MoveTR();
                    break;
                case Radar.BL:
                    MoveBL();
                    break;
                case Radar.BR:
                    MoveBR();
                    break;
                case Radar.Down:
                    MoveDown();
                    break;
                case Radar.Left:
                    MoveLeft();
                    break;
                case Radar.Right:
                    MoveRight();
                    break;
                case Radar.Up:
                default:
                    MoveUp();
                    break;
            }
        }

        public bool InRange(Drone drone)
        {
            const int distance = 750;
            return Math.Abs(drone.X - X) < distance && Math.Abs(drone.Y - Y) < distance;
        }

        public bool InRange(VisibleCreature creature, int distance = 2000)
        {
            if (!LightWasOn)
            {
                distance = 1000;
            }
            return Math.Abs(creature.X - X) < distance && Math.Abs(creature.Y - Y) < distance;
        }

        private int ToggleLight()
        {
            if (Y <= LightOnDepth)
            {
                return 0;
            }

            if (OverriddenLight)
            {
                return OverrideLight == 1 ? RapidLightCount >= 2 ? 1 : 0 : OverrideLight;
            }

            if (X <= NearEdge || X >= MaxBorder - NearEdge)
            {
                return RapidLightCount >= 2 ? 1 : 0;
            }

            return Battery > 25 ? 1 : 0;
        }

        private void MoveUp()
        {
            NextX = X;
            NextY = SaveSurfaceY;
            message = "WE FEAST!";
        }

        private void MoveDown()
        {
            NextX = X;
            NextY = Y + Drone.MaxDistancePerTurn;

            message = "CHARGE! IN DOWN DIRECTION";
        }

        private void MoveLeft()
        {
            NextX = X - Drone.MaxDistancePerTurn;
            NextY = Y;

            message = "CHARGE! IN LEFT DIRECTION";
        }

        private void MoveRight()
        {
            NextX = X + Drone.MaxDistancePerTurn;
            NextY = Y;

            message = "CHARGE! IN RIGHT DIRECTION";
        }

        private void MoveTL()
        {
            int distanceToXEdge = X - MinX;
            int distanceToYEdge = Y - MinY;

            CalculateXY(distanceToXEdge, distanceToYEdge, out int x, out int y);
            NextX = X - x;
            NextY = Y - y;

            message = "CHARGE! IN TL DIRECTION";
        }

        private void MoveTR()
        {
            int distanceToXEdge = MaxX - X;
            int distanceToYEdge = Y - MinY;

            CalculateXY(distanceToXEdge, distanceToYEdge, out int x, out int y);
            NextX = X + x;
            NextY = Y - y;

            message = "CHARGE! IN TR DIRECTION";
        }

        private void MoveBL()
        {
            int distanceToXEdge = X - MinX;
            int distanceToYEdge = MaxY - Y;

            CalculateXY(distanceToXEdge, distanceToYEdge, out int x, out int y);
            NextX = X - x;
            NextY = Y + y;

            message = "CHARGE! IN BL DIRECTION";
        }

        private void MoveBR()
        {
            int distanceToXEdge = MaxX - X;
            int distanceToYEdge = MaxY - Y;

            CalculateXY(distanceToXEdge, distanceToYEdge, out int x, out int y);
            NextX = X + x;
            NextY = Y + y;

            message = "CHARGE! IN BR DIRECTION";
        }

        private void CalculateXY(double distanceToXEdge, double distanceToYEdge, out int x, out int y)
        {
            x = 0;
            y = 0;
            if (distanceToXEdge > distanceToYEdge)
            {
                double totalDistance = distanceToXEdge + distanceToYEdge;
                totalDistance = totalDistance <= 0 ? 0.01 : totalDistance;
                double percent = distanceToYEdge / totalDistance;
                y = (int)(MaxDistancePerTurn * percent);
                x = MaxDistancePerTurn - x;
            }
            else
            {
                double totalDistance = distanceToXEdge + distanceToYEdge;
                totalDistance = totalDistance <= 0 ? 0.01 : totalDistance;
                double percent = distanceToXEdge / totalDistance;
                x = (int)(MaxDistancePerTurn * percent);
                y = MaxDistancePerTurn - x;
            }
        }
    }

    private class Creature
    {
        public int CreatureId { get; set; }

        public Creature(int creatureId)
        {
            CreatureId = creatureId;
        }
    }

    private class HiddenCreature : Creature
    {
        public Radar Radar { get; set; }
        public Drone Drone { get; set; }

        public HiddenCreature(int creatureId, Drone drone, Radar radar) : base(creatureId)
        {
            Radar = radar;
            Drone = drone;
        }
    }

    private class VisibleCreature : Creature
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int VX { get; set; }
        public int VY { get; set; }

        public VisibleCreature(int creatureId, int x, int y, int vx, int vy) : base(creatureId)
        {
            X = x;
            Y = y;
            VX = vx;
            VY = vy;
        }

        public virtual void Print()
        {
            Console.Error.WriteLine($"This creature {CreatureId} is a fishy~");
        }
    }

    private class MonsterVisibleCreature : VisibleCreature
    {
        public bool IsAngry { get; set; }
        public bool IsNotMoving => VX == 0 && VY == 0;

        public MonsterVisibleCreature(VisibleCreature creature, bool isAngry) : base(creature.CreatureId, creature.X, creature.Y, creature.VX, creature.VY)
        {
            IsAngry = isAngry;
        }

        public override void Print()
        {
            Console.Error.WriteLine($"This creature {CreatureId} is a MONSTER! {(IsAngry ? "HE MADD!" : "")}");
        }
    }

    private class Action
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public int Light { get; set; } = 0;
        public string Message { get; set; }
        public Radar Radar { get; set; }

        public Action(int? x, int? y, int light, string message, Radar radar, int count)
        {
            X = x;
            Y = y;
            Light = light;
            Message = !slience ? message : count < _words.Count ? _words[count] : "";
            Radar = radar;
        }

        public void DoAction()
        {
            if (X.HasValue && Y.HasValue)
            {
                Console.WriteLine($"Move {X} {Y} {Light} {Message}");
            }
            else
            {
                Console.WriteLine($"Wait {Light}");
            }
        }
    }
}
