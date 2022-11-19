# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.8.2] 2022-11-19
### Fixed
- `IndexOutOfRangeException` when reading to `IReadOnlyList<T>`

## [1.8.1] 2022-11-17
### Added
- missing `Object` and `Enum` read functions for Reader

## [1.8.0] 2022-11-17
### Added
- ability to Read data in as `IReadOnlyList` for all primitive and unity types

## [1.7.2] 2022-11-04
### Fixed
- issue where paths were not being built correctly for `ReadError`s
- improved error handling

## [1.7.1] 2022-11-04
### Fixed
- issue where you could no longer log errors to the `IReader`

## [1.7.0] 2022-11-03
### Added
- Improved error handling for reading `Tater` objects
- `ReadError` class containing more information about read errors than just the message

## [1.6.0] 2022-11-01
### Added
- `LineNumber` property to `Tater` objects to help refer to them during reading

## [1.5.0] 2022-10-29
### Added
- `Array` now holds a type value to enforce a type upon values contained within the `Array`
### Removed
- Idea of `null` values to reduce complexity of parsing and reading values. Should also eliminate the need of defining behaviour for null values
## [1.4.0] 2022-10-26
### Added
- setters for `Tater` objects and arrays via `this[string key]` and `this[int index]` respectively

## [1.3.1] 2022-10-23
### Fixed
- issue where array positions were not being reported properly in `Tater` objects

## [1.3.0] 2022-10-23
### Added
- `GetTater` function to `IReader` and its implementation for generic blob reading
- functionality to `Tater` blobs to better read object and array values

## [1.2.0] 2022-10-20
### Added
- `LogError` function to `IReader` and its implementations

## [1.1.1] 2022-10-16
### Fixed
- issue where `Writer` could not distinguish arrays

## [1.1.0] 2022-10-16
### Added
- ability to read into arrays

## [1.0.1] 2022-10-16
### Fixed
- issue where there were conflicting guids in .meta files

## [1.0.0] 2022-10-15
### Added
- Reader and Writer for most serializable built-in and Unity types
- `Tater` data structure for amorphous reading/writing