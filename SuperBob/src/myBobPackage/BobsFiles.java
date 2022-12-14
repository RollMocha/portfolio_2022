package myBobPackage;

import java.awt.Image;
import java.util.Random;

import javax.swing.ImageIcon;

public class BobsFiles {
	
	//사운드 파일 
	static String music_Path = "src/mp3/sample03.wav";
	static String poop_Path = "src/mp3/poop.wav";
	static String food_Path = "src/mp3/food.wav";
	static String die_Path = "src/mp3/die.wav";
	static String success_Path = "src/mp3/success.wav";
	static String clear_Path = "src/mp3/GameClear.wav";
	
	//버튼 이미지
	static Image btn_Play = new ImageIcon("src/images/play.png").getImage();
	static Image btn_Stop = new ImageIcon("src/images/stop.png").getImage();
	static Image btn_exit = new ImageIcon("src/images/btn_exit.png").getImage().getScaledInstance(200, 60, Image.SCALE_SMOOTH);
	
	//슈퍼밥 글자이미지
	static Image superBob = new ImageIcon("src/images/SuperBob_Logo.png").getImage().getScaledInstance(290, 150, Image.SCALE_SMOOTH);
	//게임오버 이미지
	static Image gameOver = new ImageIcon("src/images/GameOver.png").getImage().getScaledInstance(320, 200, Image.SCALE_SMOOTH);
	//미션 클리어 이미지(황금똥 획득)
	static Image gameClear_Back = new ImageIcon("src/images/ClearBack.jpg").getImage().getScaledInstance(AvoidingPoop.back_Width, AvoidingPoop.back_Height, Image.SCALE_SMOOTH);
	static Image gameClear2 = new ImageIcon("src/images/GameClear.png").getImage().getScaledInstance(300, 150, Image.SCALE_SMOOTH);
	//게임 시작 말풍선 이미지
	static Image gameStart = new ImageIcon("src/images/GameStart2.png").getImage().getScaledInstance(650, 300, Image.SCALE_SMOOTH);
	//타이머 이미지
	static Image timer = new ImageIcon("src/images/Timer.png").getImage().getScaledInstance(295, 290, Image.SCALE_SMOOTH);
	
	
	//Bob 이미지
	static Image bob = new ImageIcon("src/images/bob.png").getImage();
	static Image bob_Left = new ImageIcon("src/images/bob_L.png").getImage();
	static Image bob_Right = new ImageIcon("src/images/bob_R.png").getImage();
	static Image bob_Down_Left = new ImageIcon("src/images/bob_Down_Left.png").getImage();
	static Image bob_Down_Right = new ImageIcon("src/images/bob_Down_Right.png").getImage();
	static Image bob_Happy = new ImageIcon("src/images/bob_Happy.png").getImage();
	
	//Bob HP
	static Image bob_HP00 = new ImageIcon("src/images/hp0.png").getImage().getScaledInstance(250, 40, Image.SCALE_SMOOTH);
	static Image bob_HP01 = new ImageIcon("src/images/hp1.png").getImage().getScaledInstance(250, 40, Image.SCALE_SMOOTH);
	static Image bob_HP02 = new ImageIcon("src/images/hp2.png").getImage().getScaledInstance(250, 40, Image.SCALE_SMOOTH);
	static Image bob_HP03 = new ImageIcon("src/images/hp3.png").getImage().getScaledInstance(250, 40, Image.SCALE_SMOOTH);
	static Image bob_HP04 = new ImageIcon("src/images/hp4.png").getImage().getScaledInstance(250, 40, Image.SCALE_SMOOTH);
	static Image bob_HP05 = new ImageIcon("src/images/hp5.png").getImage().getScaledInstance(250, 40, Image.SCALE_SMOOTH);
	
	//배경 이미지
	static Image backGraoud = new ImageIcon("src/images/background1.png").getImage();
	//static Image backGraoud = new ImageIcon("src/images/background.gif").getImage();
	
	//똥 이미지
	static Image poops = new ImageIcon("src/images/poop" + (new Random()).nextInt(5) + ".png").getImage();
	//황금똥
	static Image gole_Poop = new ImageIcon("src/images/goldPoop.png").getImage();
	
	//음식 이미지
	static Image foods = new ImageIcon("src/images/food" + (new Random()).nextInt(10) + ".png").getImage();
	//금화
	static Image money = new ImageIcon("src/images/money.png").getImage().getScaledInstance(50, 50, Image.SCALE_SMOOTH);
	//하트
	static Image heart = new ImageIcon("src/images/heart.png").getImage().getScaledInstance(50, 50, Image.SCALE_SMOOTH);
}
