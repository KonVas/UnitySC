thisProcess.openUDPPort(57120); //make sure this port is open.

s.reboot;
s.waitForBoot{
	OSCdef(\upick, {|msg| //every pick up from the ball will make a sound
		msg.postln;
		play{
			var env = EnvGen.kr(Env.perc(0.2, 0.5), Done.freeSelf);
			SinOsc.ar(220.0, 0, env);
		};
	}, '/picktrig' );

	OSCdef(\uwin, {|msg|
		msg.postln; //a sound will play when the game is complete
		play{
			var env = EnvGen.kr(Env.perc(0.55, 6, -1), Done.freeSelf);
			LFSaw.ar({XLine.kr(120.0, 880.0, 0.3)+Rand(0.5)}!2, 0, env);
		};
	}, '/win' );
};
