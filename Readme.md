# Goal

ScriptableObjects used for variables and events

# Installation

These steps require Unity 2018.1.x or greater.

### github reference

A github reference allows a project to always use the latest version of this package. To reference the package, modify your projects \Packages\manifest.json file by changing

```
{
  "dependencies": {
    "com.unity.collab-proxy": "1.2.16",
```

to add a dependency on the SoArchitecture github repository

```
{
  "dependencies": {
    "com.feddas.soarchitecture": "https://github.com/Feddas/SoArchitecture.git",
    "com.unity.collab-proxy": "1.2.16",
```

If you run into [authentication issues, try some of the steps on this forum thread](https://forum.unity.com/threads/re-rooting-git-repository-for-package-manager.631477/).

### Local copy

1. Download a release SoArchitecture0.0.x.zip from https://github.com/Feddas/SoArchitecture/releases

2. Unzip SoArchitecture0.0.x.zip into your projects Packages folder while Unity is closed. If Unity is open during the file transfer, Unity needs to be restarted to recognize the package.

```
ProjectFolder <- This should match the name of your Unity project
 + Assets folder
 + Packages folder <- Unzip into this folder
     + SoArchitecture0.0.x <- This folder should be created from unzipping
```

3. Start Unity

[Official package manager info](https://blogs.unity3d.com/2018/05/09/unity-packages-life-cycle/).

# Usage

To learn the basics watch https://www.youtube.com/watch?v=raQ3iHhE_Kk

### Payload events

Payload events are events that carry a value when the event is raised. Any type can be supported by inheriting GameEventPayload<,,>. Types already included are bool, int, and float.

Here's an example binding a float event to a slider

1. Create a GameEventFloat - This event will be raised with a payload of the sliders current value whenever the slider is modified.
    1. Right click in project window
    2. Create => SoArchitecture => GameEventFloat
    3. Name the new event "MySliderEvent"
2. Create a new scene
3. Add a slider by right clicking in hierarchy UI => Slider
    1. Inside OnValueChanged, click the '+' button and add 'MySliderEvent' as the source object.
    2. Change the OnValueChanged function to GameEventFloat => Raise()   Be sure to select the dynamic raise at the top so the event will be fed the value selected on the slider.
4. Add a text overlay to the slider by right clicking the slider in the hierarchy and selecting UI => Text. This will be a Text UI object as a child of the slider.
    1. Add a TextStringFormat component to the text.
        1. Drag this text object into the "Output Text" field.
        1. Drag "MySliderEvent" into the `Variables To Format` field.
        2. Change the "Format" field to "You picked {0}".
    2. Add a "GameEventListenerFloat" component to the text.
        1. Drag "MySliderEvent" into the `Event` field.
        2. Inside Response, click the '+' button and add this text object
        3. Change the Response function to TextStringFormat => UpdateText()
5. Press the play button. As you move the slider the float event will be raised. As the event is raised, the text will be updated with the sliders current value.

# Inspiration

Ryan Hippies' Unite Austin 2017 talk
* [The video for the talk is posted on Unity's YouTube page.](https://www.youtube.com/watch?v=raQ3iHhE_Kk)
* [Here is a blog post he did about the talk](http://www.roboryantron.com/2017/10/unite-2017-game-architecture-with.html)
* [The slides are on slideshare.](https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects)
