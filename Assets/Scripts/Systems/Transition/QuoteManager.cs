using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuoteManager
{

    // Static Fields/Variables
    
    private static List<string> ocean_list = new List<string> { "The ocean covers 71% of the Earth's surface - USGS", // https://www.usgs.gov/programs/cmhrp/news/top-10-things-you-didnt-know-about-ocean
                                            "According to World Register of Marine Species, there are currently at least 236,878 named marine species - World Register of Marine Species", // http://www.marinespecies.org/
                                            "The record for the deepest free dive is held by Jacques Mayol. He dove to an astounding depth of 86m (282ft) without any breathing equipment - MarineBio", // https://marinebio.life/
                                            "The Atlantic Ocean is the youngest of the five oceans, having formed during the Jurassic Period approximately 150 million years ago following the breakup of the supercontinent Pangaea - Britannica" // https://www.britannica.com/
                                            };

    private static List<string> pollution_list = new List<string> { "More than 8 million tons of plastic enter the oceans every year - Earth", // https://earth.org/plastic-pollution-in-the-ocean-facts/
                                                "Ocean plastic pollution is on track to triple by 2060 and exceed one billion tons of plastic in the ocean - Earth", // https://earth.org/plastic-pollution-in-the-ocean-facts/
                                                "In 2014, California became the first state to ban plastic bags. As of March 2018, 311 local bag ordinances have been adopted in 24 states, including Hawaii. As of July 2018, 127 countries have adopted some form of legislation to regulate plastic bags - WRI", // https://www.plasticbaglaws.org/
                                                "Headline-grabbing oil spills account for just 12 percent of the oil in our oceans. Two to three times as much oil is carried out to sea via runoff from our roads, rivers and drainpipes - Conversation" // https://www.conservation.org/stories/ocean-pollution-facts
                                                };

    public static RandomDequeuer<string> ocean_queue = new RandomDequeuer<string>(ocean_list);
    public static RandomDequeuer<string> pollution_queue = new RandomDequeuer<string>(pollution_list);

    // Methods
    public static void Reset()
    {
        if (ocean_queue.IsEmpty())
            ocean_queue = new RandomDequeuer<string>(ocean_list);

        if (pollution_queue.IsEmpty())
            pollution_queue = new RandomDequeuer<string>(pollution_list);
 
    }

    public static bool IsEmpty()
    {
        return ocean_queue.IsEmpty() || pollution_queue.IsEmpty();
    }
}