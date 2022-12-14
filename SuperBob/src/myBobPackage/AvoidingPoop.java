package myBobPackage;

import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;
import java.util.Random;
import java.util.Timer;
import java.util.TimerTask;
import javax.swing.*;

public class AvoidingPoop {

	public static final int back_Width = 950;
	public static final int back_Height = 720;
	public static boolean music_Play_Chk; 
	private JFrame frame;
	private Image backImg, bobImg, bob_Happy, bob_Left, bob_Right, bob_Down_Left, bob_Down_Right, bufferImg, gold;
	private Graphics screenGraphic;
	private PlayPanel panel;
	private JPanel p_Status;
	private JLabel lbl_GameOver, lbl_Start, lbl_back, lbl_Bob, lbl_Timer, lbl_Time, lbl_Clear, lbl_GameClear, lbl_SuperBob;
	private RoundedButton btn_Play, btn_Stop, btn_Exit, btn_Music;
	private FoodItem food;
	private ArrayList<FoodItem> foodList = new ArrayList<>();
	private BobEnemy bobEnemy;
	private ArrayList<BobEnemy> poopList = new ArrayList<BobEnemy>();
	private SoundPlayer player = null;
	private SoundPlayer hitPoop_sound = null;
	private SoundPlayer hitFood_sound = null;
	private SoundPlayer hitGold_sound = null;
	private int SPEED;
	private int bob_X, bob_Y, bob_Size;
	private int gold_X, gold_Y, gold_Size;
	private boolean leftCheck, rightCheck, thread_Flug, start_Flug, gold_chk;
	private Thread thread_Bob;
	private int sleep_Bob, sleep_gold;
	private int bob_HP;
	private int foodCnt, poopCnt;
	private int timer_Count;
	private Timer timer;

	public static void main(String[] args) {
		new AvoidingPoop();
	}

	public AvoidingPoop() {
		SPEED = 10;
		bob_Size = 80;
		gold_Size = 60;
		leftCheck = rightCheck = false;
		bobImg = new ImageIcon(BobsFiles.bob.getScaledInstance(bob_Size, bob_Size, Image.SCALE_SMOOTH)).getImage();
		bob_Happy = new ImageIcon(BobsFiles.bob_Happy.getScaledInstance(100, 100, Image.SCALE_SMOOTH)).getImage();
		bob_Left = new ImageIcon(BobsFiles.bob_Left.getScaledInstance(bob_Size, bob_Size, Image.SCALE_SMOOTH)).getImage();
		bob_Right = new ImageIcon(BobsFiles.bob_Right.getScaledInstance(bob_Size, bob_Size, Image.SCALE_SMOOTH)).getImage();
		bob_Down_Left = new ImageIcon(BobsFiles.bob_Down_Left.getScaledInstance(bob_Size, bob_Size, Image.SCALE_SMOOTH)).getImage();
		bob_Down_Right = new ImageIcon(BobsFiles.bob_Down_Right.getScaledInstance(bob_Size, bob_Size, Image.SCALE_SMOOTH)).getImage();
		backImg = new ImageIcon(BobsFiles.backGraoud.getScaledInstance(back_Width, back_Height, Image.SCALE_SMOOTH)).getImage();
		gold = new ImageIcon(BobsFiles.gole_Poop.getScaledInstance(gold_Size, gold_Size, Image.SCALE_SMOOTH)).getImage();
		bob_X = back_Width / 2;
		bob_Y = back_Height - 150;
		gold_X = new Random().nextInt(back_Width - gold_Size);
		gold_Y = 0 - gold_Size;
		thread_Flug = true;
		start_Flug = false;
		sleep_Bob = 20;
		sleep_gold = 0;
		gold_chk = false;
		bob_HP = 5;
		frame = new JFrame();
		player = new SoundPlayer(BobsFiles.music_Path);
		music_Play_Chk = true;
		foodCnt = 5;	//떨어지는 과일 수
		poopCnt = 10;	//떨어지는 똥의 수
		timer_Count = 60;
		init();
	}

	// 프레임 설정
	private void init() {
		
		// 게임패널
		panel = new PlayPanel();
		panel.setLayout(null);
		panel.addKeyListener(new MyMovingTextKeyListener());
		panel.setFocusable(true);
		panel.requestFocus();
		panel.setBounds(0, 0, 950, 720);
		panel.setVisible(false);
		frame.getContentPane().add(panel);

		// 게임 시작 전 패널
		lbl_back = new JLabel(new ImageIcon("src/images/background.gif"));
		lbl_back.setBounds(0, 0, 950, 720);
		lbl_back.setVisible(true); //수정
		frame.getContentPane().add(lbl_back);

		// 시작 라벨
		lbl_Start = new JLabel(new ImageIcon(BobsFiles.gameStart));
		lbl_Start.setBounds(220, 120, BobsFiles.gameStart.getWidth(null), BobsFiles.gameStart.getHeight(null));
		lbl_back.add(lbl_Start);

		// 밥 이미지
		lbl_Bob = new JLabel(new ImageIcon(BobsFiles.bob));
		lbl_Bob.setBounds(350, 470, BobsFiles.bob.getWidth(null), BobsFiles.bob.getHeight(null));
		lbl_back.add(lbl_Bob);
		

		//게임 클리어 배경이미지
		lbl_Clear = new JLabel(new ImageIcon(BobsFiles.gameClear_Back));
		lbl_Clear.setBounds(0, 0, back_Width, back_Height);
		lbl_Clear.setVisible(false); //수정
		frame.getContentPane().add(lbl_Clear);

		
		//게임 클리어 : 기뻐하는 밥
		JLabel lbl_HappyBob = new JLabel();
		lbl_HappyBob.setBounds(500, 300, 200, 200);
		lbl_HappyBob.setIcon(new ImageIcon(BobsFiles.bob_Happy.getScaledInstance(200, 200, Image.SCALE_SMOOTH)));
		lbl_Clear.add(lbl_HappyBob);

		// 현황패널
		p_Status = new JPanel();
		p_Status.setLayout(null);
		p_Status.setBounds(950, 0, 330, 720);
		p_Status.setOpaque(true);
		p_Status.setBackground(Color.decode("#EAFAF1"));
		p_Status.setVisible(true);
		frame.getContentPane().add(p_Status);
		
		// 슈퍼밥 라벨
		lbl_SuperBob = new JLabel(new ImageIcon(BobsFiles.superBob));
		lbl_SuperBob.setBounds(950, 10, 320, 150);
		lbl_SuperBob.setVisible(true);
		p_Status.add(lbl_SuperBob);
		
		//타이머
		lbl_Timer = new JLabel();
		lbl_Timer.setIcon(new ImageIcon(BobsFiles.timer));
		lbl_Timer.setHorizontalAlignment(JLabel.CENTER);
		lbl_Timer.setBounds(960, 160, 295, 290);
		lbl_Timer.setFont(new Font("맑은 고딕", Font.BOLD, 30));
		//lbl_Timer.setOpaque(true);
		//lbl_Timer.setBackground(Color.decode("#485812"));
		lbl_Timer.setForeground(Color.decode("#da3210"));
		//lbl_Timer.setBorder(tb);
		lbl_Timer.setVisible(false);
		p_Status.add(lbl_Timer);
		
		//타임
		lbl_Time = new JLabel();
		lbl_Time.setBounds(960, 190, 295, 290);
		lbl_Time.setFont(new Font("맑은 고딕", Font.BOLD, 60));
		lbl_Time.setHorizontalAlignment(JLabel.CENTER);
		lbl_Time.setForeground(Color.decode("#da3210"));
		lbl_Timer.setVisible(false);
		p_Status.add(lbl_Time);

		// 게임클리어 라벨
		lbl_GameClear = new JLabel(new ImageIcon(BobsFiles.gameClear2));
		lbl_GameClear.setBounds(950, 160, 320, 150);
		lbl_GameClear.setVisible(false);
		p_Status.add(lbl_GameClear);
		
		// 게임오버 라벨
		lbl_GameOver = new JLabel(new ImageIcon(BobsFiles.gameOver));
		lbl_GameOver.setBounds(960, 160, 320, 200);
		lbl_GameOver.setVisible(false);
		p_Status.add(lbl_GameOver);

		// 배경음악 재생버튼
		btn_Music = new RoundedButton("ON");
		btn_Music.setBounds(980, 500, 70, 50);
		btn_Music.setBorder(null);
		btn_Music.setBackground(Color.decode("#23A8DB"));
		btn_Music.setFont(new Font("맑은 고딕", Font.BOLD, 20));
		btn_Music.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
		btn_Music.addActionListener(new BtnActionListener(player));
		
		// 게임 시작 버튼
		btn_Play = new RoundedButton("게임 시작");
		btn_Play.setBounds(980, 560, 250, 60);
		btn_Play.setBackground(Color.decode("#23A8DB"));
		btn_Play.setFont(new Font("맑은 고딕", Font.BOLD, 20));
		btn_Play.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
		btn_Play.setBorder(null);
		btn_Play.addActionListener(e -> {
			if (!start_Flug) {
				panel.setVisible(true);
				bob_HP = 5;
				startSetting();
				gameStart();

				btn_Play.setText("홈으로");
				timer = new Timer();
				timer.schedule(new MyTimerTask(), 0, 1000);
				
				
			}else {
				// 음악 정지
				try {
					player.stop();
				} catch (Exception e1) {
					e1.printStackTrace();
				}
				thread_Flug = false;
				frame.dispose();
				new AvoidingPoop();
			}
		});

		// 게임 일시정지 버튼
		btn_Stop = new RoundedButton();
		btn_Stop.setText("||");
		btn_Stop.setBounds(980, 630, 50, 50);
		btn_Stop.setBackground(Color.decode("#23A8DB"));
		btn_Stop.setFont(new Font("맑은 고딕", Font.BOLD, 20));
		btn_Stop.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
		btn_Stop.setEnabled(false);
		btn_Stop.addActionListener(e -> {

			switch(btn_Stop.getText()) {
			case "||":
				thread_Flug = false;
				btn_Stop.setText("▶");
				timer.cancel();
				break;
				
			case "▶" :
				thread_Flug = true;
				btn_Stop.setText("||");

				timer = new Timer();
				timer.schedule(new MyTimerTask(), timer_Count, 1000);
				gameStart();
				break;
			}
		});

		// 게임 나가기 버튼
		btn_Exit = new RoundedButton("나가기");
		btn_Exit.setBounds(1040, 630, 190, 50);
		btn_Exit.setFont(new Font("맑은 고딕", Font.BOLD, 20));
	    btn_Exit.setBackground(Color.decode("#23A8DB"));
		btn_Exit.setBorder(null);
		btn_Exit.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
		btn_Exit.addActionListener(e -> {
			// 음악 정지
			try {
				player.stop();
			} catch (Exception e1) {
				e1.printStackTrace();
			}
			thread_Flug = false;
			timer.cancel();
			frame.dispose();
		});
		p_Status.add(btn_Play);
		p_Status.add(btn_Stop);
		p_Status.add(btn_Exit);
		p_Status.add(btn_Music);

		// 프레임 설정
		frame.setTitle("똥 피하기 게임");
		frame.setSize(1280, 750);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setResizable(false);
		frame.setLocationRelativeTo(null);
		frame.setVisible(true);
		frame.getContentPane().setLayout(null);
		try {
			UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");// LookAndFeel Windows 스타일 적용
			SwingUtilities.updateComponentTreeUI(frame);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// 게임 시작 : Thread 시작
	private void gameStart() {

		thread_Bob = new Thread(new Runnable() {

			@Override
			public void run() {
				while (thread_Flug) {
					try {
						Thread.sleep(sleep_Bob);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}
					panel.requestFocus();
					keyPressCheck();
					poop_hitCheck(); // 밥이 똥에 맞았을때
					food_hitCheck(); // 밥이 아이템을 먹었을때
					gold_hitCheck(); // 밥이 아이템을 먹었을때
				}
			}
		});
		thread_Bob.start();

		Thread thread_item = new Thread(new Runnable() {

			@Override
			public void run() {
				while (thread_Flug) {
					try {
						Thread.sleep(100);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}
					item_Move("food");
					itemBoundaryCheck("food");
					item_Move("poop");
					itemBoundaryCheck("poop");
					item_Move("money");
					item_Move("heart");

					sleep_gold += 100;
					if (sleep_gold >= 45000) {
						gold_chk = true;
						sleep_gold = 0;
					}
					if (gold_chk) {
						item_Move("gold");
						itemBoundaryCheck("gold");
					}
				}
			}
		});
		thread_item.start();
	}

	// 시작 설정
	private void startSetting() {

		//배경음악 시작
		if(!music_Play_Chk) {
			System.out.println(music_Play_Chk);
			try {
				player = new SoundPlayer(BobsFiles.music_Path);
				player.play();
				music_Play_Chk = true;
				btn_Music.setText("ON");
			} catch (Exception e1) {
				e1.printStackTrace();
			}
		}

		lbl_Timer.setVisible(true);
		lbl_Time.setVisible(true);
		lbl_GameOver.setVisible(false);
		lbl_back.setVisible(false);
		lbl_Clear.setVisible(false);
		lbl_Timer.setVisible(true);
		//btn_Play.setEnabled(false);	
		btn_Stop.setEnabled(true);	
		thread_Flug = true;
		start_Flug = true;	
		
	}
	// 종료 설정
	private void endSetting() {
		if(music_Play_Chk) {
		//배경음악 종료
			try {
				player.stop();
				music_Play_Chk = false;
				btn_Music.setText("OFF");
			} catch (Exception e1) {
				e1.printStackTrace();
			}
		}
		lbl_Timer.setVisible(false);
		lbl_Time.setVisible(false);
		btn_Play.setEnabled(true);
		btn_Stop.setEnabled(false);	
		thread_Flug = false;
	}

	// 키 눌렀을때 이동 이벤트
	private void keyPressCheck() {
		if (leftCheck && bob_X - SPEED > 0) {
			bobImg = bob_Left;
			bob_X -= SPEED;
		}
		if (rightCheck && (bob_X + bob_Size + SPEED) < back_Width) {
			bobImg = bob_Right;
			bob_X += SPEED;
		}
	}

	// 키 눌렀을때 이벤트
	private class MyMovingTextKeyListener extends KeyAdapter {

		@Override
		public void keyPressed(KeyEvent e) {

			switch (e.getKeyCode()) {
			case KeyEvent.VK_LEFT:
				leftCheck = true;
				break;
			case KeyEvent.VK_RIGHT:
				rightCheck = true;
				break;
			}
		}

		@Override
		public void keyReleased(KeyEvent e) {
			switch (e.getKeyCode()) {
			case KeyEvent.VK_LEFT:
				leftCheck = false;
				break;
			case KeyEvent.VK_RIGHT:
				rightCheck = false;
				break;
			}
		}
	}

	// 아이템 추가
	private void item_Add(String str) {
		switch (str) {
		case "food":
			int foodX = new Random().nextInt(back_Width - FoodItem.food_Size);
			int foodY = 0 - FoodItem.food_Size;
			foodList.add(new FoodItem(foodX, foodY));
			break;

		case "poop":
			int poopX = new Random().nextInt(back_Width - BobEnemy.poop_size);
			int poopY = 0 - BobEnemy.poop_size;
			poopList.add(new BobEnemy(poopX, poopY));
			break;
		}
	}

	// 아이템 움직임
	private void item_Move(String str) {

		switch (str) {
		case "food":
			for (int i = 0; i < foodList.size(); i++) {
				food = foodList.get(i);
				food.move();
			}
			break;
		case "poop":
			for (int i = 0; i < poopList.size(); i++) {
				bobEnemy = poopList.get(i);
				bobEnemy.move();
			}
			break;
		case "gold":
			int speed = 5 + new Random().nextInt(6) * 5;
			gold_Y += speed;
			break;
		}
	}

	// 아이템 바운더리
	private void itemBoundaryCheck(String str) {
		switch (str) {
		case "food":
			for (int i = 0; i < foodList.size(); i++) {
				food = foodList.get(i);
				if (food.y > back_Height) {
					foodList.remove(i);
					item_Add("food");
				}
			}
			break;

		case "poop":
			for (int i = 0; i < poopList.size(); i++) {
				bobEnemy = poopList.get(i);
				if (bobEnemy.y > back_Height) {
					poopList.remove(i);
					item_Add("poop");
				}
			}
			break;
		case "gold":
			if (gold_Y > back_Height) {
				gold_X = new Random().nextInt(back_Width - gold_Size);
				gold_Y = 0 - gold_Size;
				gold_chk = false;
			}

		}

	}

	// 음식에 닿았을때
	private void food_hitCheck() {
		for (int i = 0; i < foodList.size(); i++) {
			food = foodList.get(i);
			if (food.x < bob_X + bob_Size && food.x + food.width > bob_X && food.y + food.height > bob_Y
					&& bob_Y + bob_Size > food.y) {
				// 사운드 플레이
				try {
					hitFood_sound = new SoundPlayer(BobsFiles.food_Path);
					hitFood_sound.play();

				} catch (Exception e) {
					e.printStackTrace();
				}
				foodList.remove(i);
				item_Add("food");
				bobImg = bob_Happy;
				if (bob_HP < 5) {
					bob_HP ++;
				}
			}
		}
	}

	// 똥에 닿았을 때
	private void poop_hitCheck() {

		for (int i = 0; i < poopList.size(); i++) {
			bobEnemy = poopList.get(i);

			if (bobEnemy.x < bob_X + bob_Size && bobEnemy.x + bobEnemy.width > bob_X
					&& bobEnemy.y + bobEnemy.height > bob_Y && bob_Y + bobImg.getHeight(null) > bobEnemy.y) {
				// 사운드 플레이
				try {
					hitPoop_sound = new SoundPlayer(BobsFiles.poop_Path);
					hitPoop_sound.play();

				} catch (Exception e) {
					e.printStackTrace();
				}

				poopList.remove(i);
				item_Add("poop");
				if(leftCheck) {
					bobImg = bob_Down_Left;
				}else {
					bobImg = bob_Down_Right;
				}
				
				leftCheck = rightCheck = false;
				new Thread(new Runnable() {

					@Override
					public void run() {
						sleep_Bob = 1000;
						try {
							Thread.sleep(1000);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
						sleep_Bob = 20;
					}
				}).start();
				bob_HP--;
				if (bob_HP <= 0) {
					endSetting();
					
					// 실패사운드
					try {
						player.stop();
						SoundPlayer gameOver = new SoundPlayer(BobsFiles.die_Path);
						gameOver.play();
						timer.cancel();

					} catch (Exception e) {
						e.printStackTrace();
					}
					//게임오버 라벨표시
					lbl_GameOver.setVisible(true);
				}
			}
		}
	}

	// 황금똥에 닿았을 때
	private void gold_hitCheck() {
		if (gold_X < bob_X + bob_Size && gold_X + gold_Size > bob_X && gold_Y + gold_Size > bob_Y
				&& gold_Y < bob_Y + bob_Size) {
			// 사운드 플레이
			try {
				player.stop();
				hitGold_sound = new SoundPlayer(BobsFiles.clear_Path);
				hitGold_sound.play();

			} catch (Exception e) {
				e.printStackTrace();
			}
			endSetting();

			timer.cancel();
			//게임클리어 라벨표시
			lbl_Clear.setVisible(true);
			lbl_GameClear.setVisible(true);
			panel.setVisible(false);
			gold_X = new Random().nextInt(back_Width - gold_Size);
			gold_Y = 0 - gold_Size;
			
		}
	}

	private class MyTimerTask extends TimerTask{
		
		@Override
		public void run() {
			lbl_Time.setText(timer_Count + "");
			timer_Count--;
			
			if(timer_Count == 0) {
				cancel();
				// 실패사운드
				try {
					player.stop();
					SoundPlayer gameOver = new SoundPlayer(BobsFiles.die_Path);
					gameOver.play();
					timer.cancel();

				} catch (Exception e) {
					e.printStackTrace();
				}
				lbl_GameOver.setVisible(true);
				endSetting();
			}
		}
	}
	// 게임 패널 설정
	public class PlayPanel extends JPanel {
		private static final long serialVersionUID = 1L;

		public PlayPanel() {

			for (int i = 0; i < foodCnt; i++) {
				int foodX = new Random().nextInt(back_Width - FoodItem.food_Size);
				int foodY = 0 - FoodItem.food_Size;
				food = new FoodItem(foodX, foodY);
				foodList.add(food);
			}
			for (int i = 0; i < poopCnt; i++) {
				int poopX = new Random().nextInt(back_Width - FoodItem.food_Size);
				int poopY = 0 - BobEnemy.poop_size;
				bobEnemy = new BobEnemy(poopX, poopY);
				poopList.add(bobEnemy);
			}

		}

		@Override
		public void paint(Graphics g) {
			bufferImg = createImage(back_Width, back_Height);
			screenGraphic = bufferImg.getGraphics();
			screenDraw(screenGraphic);
			g.drawImage(bufferImg, 0, 0, null);
		}

		public synchronized void screenDraw(Graphics g) {
			g.drawImage(backImg, 0, 0, this); // 배경이미지
			g.drawImage(bobImg, bob_X, bob_Y, this); // 밥 이미지
			g.setColor(Color.RED);
			//음식 이미지
			for (int i = 0; i < foodList.size(); i++) {
				food = foodList.get(i);
				g.drawImage(food.img, food.x, food.y, this);
			}
			//똥 이미지
			for (int i = 0; i < poopList.size(); i++) {
				bobEnemy = poopList.get(i);
				g.drawImage(bobEnemy.image, bobEnemy.x, bobEnemy.y, this);
			}
			
			//황금똥 이미지
			g.drawImage(gold, gold_X, gold_Y, this);
			
			switch(bob_HP) {
			case 0:
				g.drawImage(BobsFiles.bob_HP00, 700, 20, this);
				break;
			case 1:
				g.drawImage(BobsFiles.bob_HP01, 700, 20, this);
				break;
			case 2:
				g.drawImage(BobsFiles.bob_HP02, 700, 20, this);
				break;
			case 3:
				g.drawImage(BobsFiles.bob_HP03, 700, 20, this);
				break;
			case 4:
				g.drawImage(BobsFiles.bob_HP04, 700, 20, this);
				break;
			case 5:
				g.drawImage(BobsFiles.bob_HP05, 700, 20, this);
				break;
			}
			
			this.repaint();
		}
	}
}