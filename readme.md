# LAXR

First adventure into making some specific Unity tools that will eventually be designed to work across all interfaces including XR. LAXR is the Lazarsfeld apparatus for XR. [Paul Lazarsfeld](https://en.wikipedia.org/wiki/Paul_Lazarsfeld) is credited for being one of the founders of the modern day survey tool. I need some survey tools that work on current XR frameworks and I work in Unity so here we be!

## General Design & Basic Information

Currently using a simple data oriented design - in which my data are Unity ScriptableObject files tied to custom Structs. This is a quick way to maintain references across Unity Projects and not run into weird class dependency problems. I then am using MonoBehaviour derived classes to setup Unity visuals tied to Canvas Objects and a bunch of misc. UI prefabs I've built to work with it. Eventually I will have some editor scripts to go along with helping you create your own - for the time being it's very manual and tedious but once you figure it out, it should save you some time on simple things! See the ChangeLog.md for more detail updates and additional notes. This is currently in an unreleased state - as I am working through figuring out best/better practices with Unity and the Package Manager. Please see the current video that goes along with unrelease version 0.2.0

## Install & Dependencies

* For Normal Unity Setup
* 🚀 📺 [Unlisted YouTube Video for a quick walk through](https://youtu.be/3nW1QMGXDGk)
* For MRTK Use Cases **VERY IMPORTANT**
  * You need to have a fresh Unity project already configured with [MRTK 2.7.2](https://github.com/microsoft/MixedRealityToolkit-Unity)
  * Some minor [MRTK Bug Fixes](https://github.com/microsoft/MixedRealityToolkit-Unity/pull/9938/commits/b199b3459298f31a599c28ab7863d0abb1a5acdf)
    * This should be fun!
* Unity Package Manager to the rescue... well I hope 

## Active Development Dates

* November 2019
  * Initial framework for themes, pages, layouts, and fonts
  * All tied to structs - not sure if I'm going to pull these out - combine them with scriptable assets - or who knows, get real crazy and go full ECS
* August 2021
  * Well it's been a minute! Sort of forgot about this work I was doing, think it's still usable going to update a few things and see if I can pump out something that makes sense.
* November 2021
  * Lazarsfeld is almost alive!
    * Data tool for procedurally generating a diegetic simple survey apparatus
      * Likert based questions (working) - simple version is working
      * JSON serialized class tied to a series of scriptable objects and prefabs (partially working)
* February 2022
  * Updates to include generic Layout panels
  * Still haven't finalized all the other question types as I'm doing this work after hours

## License Notes

* This software running a dual license
* Most of the work this repository holds is driven by the development process from the team over at Unity3D :heart: to their never ending work on providing fantastic documentation and tutorials that have allowed this to be born into the world.
* I personally feel that software and it's practices should be out in the public domain as often as possible, I also strongly feel that the capitalization of people's free contribution shouldn't be taken advantage of.
  * If you want to use this software to generate a profit for you/business I feel that you should equally 'pay up' and in that theory I support strong copyleft licenses.
  * If you feel that you cannot adhere to the GPLv3 as a business/profit please reach out to me directly as I am willing to listen to your needs and there are other options in how licenses can be drafted for specific use cases, be warned: you probably won't like them :rocket:

### Educational and Research Use MIT Creative Commons

* If you are using this at a Non-Profit and/or are you yourself an educator and want to use this for your classes and for all student use please adhere to the MIT Creative Commons License
* If you are using this back at a research institution for personal research and/or funded research please adhere to the MIT Creative Commons License
  * If the funding line is affiliated with an [SBIR](https://www.sbir.gov) be aware that when/if you transfer this work to a small business that work will have to be moved under the secondary license as mentioned below.

### Commercial and Business Use GPLv3 License

* For commercial/business use please adhere by the GPLv3 License
* Even if you are giving the product away and there is no financial exchange you still must adhere to the GPLv3 License

## Contact

* [John Shull](mailto:the.john.shull@gmail.com)
* [Twitter](https://twitter.com/TheJohnnyFuzz)
