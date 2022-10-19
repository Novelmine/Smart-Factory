import cv2
import time
video_cap = cv2.VideoCapture(0)
#video_cap = cv2.VideoCapture('파일경로 입력')
def videoReady():
    # 캠 사용가능 여부 체크
    if not video_cap.isOpened:
        print('카메라가 정상 작동되지않습니다')
        exit(0)
    #카메라 크기 설정    
    video_cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
    video_cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
    
    
videoReady()

def videoPlay(img):
    cv2.imshow("video test", img)
    

#카메라 읽은값을 출력    
while True:
    ret, frame = video_cap.read()
    print(ret, frame)
    #읽은 이미지를 출력
    cv2.imshow("video test", frame)
    # frame 정보가 없다면 영상이 없습니다 문구 출력
    if frame is None:
        print('영상이 없습니다')
        #메모리 정리하기
        video_cap.release()
        break
    videoPlay(frame);
    #아무키나 누를때까지 계속 실행
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# 메모리 정리
cv2.waitKey(0)
cv2.destroyAllWindows()