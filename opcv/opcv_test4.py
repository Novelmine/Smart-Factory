import cv2
print("OpenCV version:")
print(cv2.__version__)
img = cv2.imread("image/bts-01.jpg")
#이미지 형태 픽셀크기
print("image shape: {} pixels".format(img.shape))
#이미지 가로 크기
print("width: {} pixels".format(img.shape[1]))
#이미지 세로크기
print("height: {} pixels".format(img.shape[0]))
#이미지의 컬러여부 3이면 컬러 1이면 흑백
print("channels: {}".format(img.shape[2]))
#컬러이미지 출력하기
cv2.imshow("bts", img)
# 순서 b, g, r 순서를 지켜야 함(0~255)
(b, g, r) = img[0, 0]
print("Pixel at (0, 0) - Red: {}, Green: {}, Blue: {}".format(r, g, b))
cv2.waitKey(0)
# 세로 50~100, 가로 200~400 : px의 절대적 위치
dot = img[50:100, 200:400]
cv2.imshow("Dot", dot)
cv2.waitKey(0)
#(b, g, r)
img[50:100, 200:400] = (255, 0, 0)
img[100:200, 200:400] = (100, 100, 0)
cv2.imshow("bts - dotted", img)
cv2.waitKey(0)
cv2.destroyAllWindows()