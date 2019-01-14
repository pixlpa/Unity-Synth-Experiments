# Unity-Synth-Experiments

A set of simple generative sound engines using OnAudioFilterRead() in Unity. I borrowed pretty heavily from https://github.com/JZito/Unity-Synthesizer-in-C-Sharp to get started, but ultimately reorganized things so each engine is pretty self contained. The scripts inside of Scripts/SoundEngines/ are the fun part. Add one of those to a gameObject along with an AudioSource and you are in business. You will need to make a script that calls the KeyOn() method to activate the sounds. The inspectors for these are very minimal, but there should be plenty there to play with and extend.

## pxFemme
A quirky 2-op FM design with feedback on both the modulator and the carrier. Makes some good growly sounds.

## pxSnarple
Simple loopy sample player/synth loosely inspired by the Volca Sample in loop mode

## pxStrax
2-osc subtractive square+triangle synth design with a Moog-style VCF and waveshaping distortion


