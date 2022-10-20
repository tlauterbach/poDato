# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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