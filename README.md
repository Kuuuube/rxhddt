# RXHDDT

An easy-to-use osu replay editor to modify the mods from the command line. Only supports std mode replays.

Thanks to the osu!ftw replay editor for providing code to read and write replays.

## Downloads

Downloads are available in [Releases](https://github.com/Kuuuube/rxhddt/releases).

## How it works

1. Start the program and enter the path to the replay file you want to edit.

2. Change the mods by pressing the corresponding keys on your keyboard.

3. Press enter and enter a path to save the edited replay.

## Building:

Run the following in powershell:

```
$options= @('--configuration', 'Release', '-p:PublishSingleFile=true', '-p:DebugType=embedded', '--self-contained', 'false')
dotnet publish rxhddt $options --runtime win-x64 --framework net6.0 -o build/win-x64
dotnet publish rxhddt $options --runtime linux-x64 --framework net6.0 -o build/linux-x64
```