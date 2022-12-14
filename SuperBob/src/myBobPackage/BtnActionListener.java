package myBobPackage;

import java.awt.event.*;
import javax.swing.*;

public class BtnActionListener implements ActionListener{

	SoundPlayer player;
	
	public BtnActionListener() {
	}

	public BtnActionListener(SoundPlayer player) {
		this.player = player;
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		JButton btn = (JButton) e.getSource();
		
		switch(btn.getText()) {
		case "OFF":
			try {
				player.play();
				AvoidingPoop.music_Play_Chk = true;
				btn.setText("ON");
			} catch (Exception e1) {
				e1.printStackTrace();
			}
			break;
			
		case "ON":
			try {
				player.pause();
				AvoidingPoop.music_Play_Chk = false;
				btn.setText("OFF");
			} catch (Exception e1) {
				e1.printStackTrace();
			}
			break;
		}
	}
}
