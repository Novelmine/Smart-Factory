from tensorflow.keras.models import load_model
from PIL import Image, ImageOps
import numpy as np

# Load the model
model = load_model('keras_model.h5')

#테스트 데이터를 저장할 변수의 타입과 shape 지정
data = np.ndarray(shape=(1, 224, 224, 3), dtype=np.float32)
# 테스트 이미지 path 정의
image = Image.open('C:/int/dnn_basic/image/maa.jpg').convert('RGB')
# 기본 이미지 사진 정의
size = (224, 224)
image = ImageOps.fit(image, size, Image.ANTIALIAS)
#lavles.txt 파일로딩
file = open('C:/int/dnn_basic/teachable_ml/labels.txt', "r" , encoding='UTF8')
class_names = file.readlines()
file.close()

#turn the image into a numpy array
image_array = np.asarray(image)
# Normalize the image
normalized_image_array = (image_array.astype(np.float32) / 127.0) - 1
# Load the image into the array
data[0] = normalized_image_array

# 예측값 수행
prediction = model.predict(data)
index = np.argmax(prediction)
class_name = class_names[index]
confidence_score = prediction[0][index]

#print("prediction" , prediction)
#print("np.argmax(prediction) : ", index)
print("Class: ", class_name)
#print("Confidence Score: ", confidence_score)
