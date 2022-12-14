package myBobPackage;

import java.io.File;
import javax.sound.sampled.*;

public class SoundPlayer {
	Long nowFrame;
	Clip clip;
	String status;
	AudioInputStream audioStream;
	String path;
	
	public SoundPlayer(String path) {
		this.path = path;
		try {
			init();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private void init() throws Exception{
		audioStream = AudioSystem.getAudioInputStream(new File(path).getAbsoluteFile());
		clip = AudioSystem.getClip();
		clip.open(audioStream);
		if(path.equals(BobsFiles.music_Path)) {
			clip.loop(Clip.LOOP_CONTINUOUSLY);
		}
	}
	//재생
	public void play(){
		clip.start();
	}
	//일시정지
	public void pause() {
		this.nowFrame = this.clip.getMicrosecondPosition();
		clip.stop();
	}
	//이어서 재생
	public void resumeAudio() throws Exception{
		clip.close();
		resetAudioStream();
		clip.setMicrosecondPosition(nowFrame);
		this.play();
	}
	//오디오 끔
	public void stop() throws Exception{
		nowFrame = 0L;
		clip.stop();
		clip.close();
	}
	//오디오 리셋
	public void resetAudioStream() throws Exception{
		audioStream = AudioSystem.getAudioInputStream(new File(path).getAbsoluteFile());
		clip.open(audioStream);
		clip.loop(Clip.LOOP_CONTINUOUSLY);
	}
}
