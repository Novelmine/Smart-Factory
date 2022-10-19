from configparser import Interpolation
import fractions
from tensorflow.keras.models import load_model
from PIL import Image, ImageOps
import numpy as np
import cv2
import pygame
size = (224, 224)

#소리파일 로딩
pygame.mixer.init()
pygame.mixer.music.load('C:/int/dnn_basic/image/no.MP3')


# Load the model
model = load_model('keras_model.h5')
#테스트 데이터를 저장할 변수의 타입과 shape 지정
data = np.ndarray(shape=(1, 224, 224, 3), dtype=np.float32)
file = open('C:/int/dnn_basic/teachable_ml/labels.txt', "r" , encoding='UTF8')
class_names = file.readlines()
file.close()

video_cap = cv2.VideoCapture(0)
#video_cap = cv2.VideoCapture('파일경로 입력')
#def videoReady():
    # 캠 사용가능 여부 체크
  # if not video_cap.isOpened:
    #    print('카메라가 정상 작동되지않습니다')
    #    exit(0)
    #카메라 크기 설정    
   # video_cap.set(cv2.CAP_PROP_image_WIDTH, 640)
   #video_cap.set(cv2.CAP_PROP_image_HEIGHT, 480)
    
    
#videoReady()

def videoPlay(img):
    cv2.imshow("video test", img)
    

#카메라 읽은값을 출력    
while True:
    if not video_cap.isOpened:
        print('카메라가 정상 작동되지않습니다')
        exit(0)
    ret, image = video_cap.read()
    #읽은 이미지를 출력
    image.astype(np.float32)
    # 기본 이미지 사진 정의
    image_resized = cv2.resize(image, dsize=size, interpolation=cv2.INTER_AREA)
    #turn the image into a numpy array
    image_array = np.asarray(image_resized)
    # Normalize the image
    normalized_image_array = (image_array.astype(np.float32) / 127.0) - 1
    # Load the image into the array
    data[0] = normalized_image_array
    # 예측값 수행
    prediction = model.predict(data)
    index = np.argmax(prediction)
    class_name = class_names[index]
    confidence_score = prediction[0][index];
    
    if index == 1 :
        if (pygame.mixer.music.get_busy() == False):
            pygame.mixer.music.play()
        
        image = cv2.cvtColor(image, cv2.COLOR_BGR2Lab)
    font_color = (0 , 0, 255)
    font = cv2.FONT_HERSHEY_TRIPLEX
    font_scale = 1
    image = cv2.putText(image, class_name , (10,40), font , font_scale, font_color)
    print("Class: ", class_name)

    cv2.imshow("video test", image)
    # image 정보가 없다면 영상이 없습니다 문구 출력
    if image is None:
        print('영상이 없습니다')
        #메모리 정리하기
        video_cap.release()
        break
    videoPlay(image);
    #아무키나 누를때까지 계속 실행
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# 메모리 정리
cv2.waitKey(0)
cv2.destroyAllWindows()
