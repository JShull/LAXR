# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.2.0] - 2022-02-06

### Added

- [@JShull](https://github.com/jshull).
  - Updated and Converted base classes to be easily extended
  - FL_InfoPanel.cs
    - MonoBehaviour derived class that lets you build simple one panel information pages
    - Point it at your scriptable object data and the rest manages itself
  - Lz_Info.cs
    - Is the new base ScriptableObject derived class that lets you put together the data you need for all generic layout information - all additional high level ScriptableObjects will derive from this
    - Pass this data object to the FL_InfoPanel.cs
  - LZ_Panel.cs
    - Is the ScriptableObject class that derives from Lz_Info.cs - this is the added data we need for what is considered the basic/generic panel.
  - Editor script added to make it easier to navigate how you want to use it
  - Added high level collection of objects, now you can have multiple likert under one collection, etc.

### Changed

- Script Naming Conventions to match Unity Packages
- L_Survey.cs is using the added new interface for IPageSetup
- Likert_Page_Prefab
  - Changed Canvas Scaler to be constant with screen size
- FL_page.cs modifications to initialization
  - Now have to pass a camera and if you want this to be in world space or not
- Underlying ScriptableObject classes have been adjusted.
  - Main update: change to the existing Lz_Likert.cs ScriptableObject, this now derives from a new ScriptableObject base class called Lz_Info.cs that was added
  - For the most part going forward anything that deals with a layout is going to need the sort of baseline information that is accounted for in the Lz_Info.cs
    - Header / SubHeader / Font / Theme / Footer
    - Anything else will be a special ScriptableObject that derives from this, see the changes to Lz_Likert as a reference.
- Using Unity 2021.2.10f1 to build this package, should work in lower levels of Unity but I haven't tested it.

### Fixed

- Fixed a bug related to disabling the FL_Page component - if this item had an Event Data on it it was not re-added after it was enabled

### Removed

- What Exactly: List out

[Unreleased]: https://github.com/jshull/LAXR/Readme.md
[0.2.0]: https://github.com/jshull/LAXR.git

## [0.1.0] - 2021-12-10

### Added

- [@JShull](https://github.com/jshull).
  - Simple Likert Example
  - Unity Scriptable Objects
  - Unity Package Manager Setup
  - Basic Documentation

### Changed

- Started using changelog
- Updated namespaces to better align with project folders

### Removed

- MRTK direct dependencies

[Unreleased]: https://github.com/jshull/LAXR.git

[0.1.0]: https://github.com/jshull/LAXR.git