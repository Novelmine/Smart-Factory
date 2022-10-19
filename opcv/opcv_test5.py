import cv2
print("OpenCV version:")
print(cv2.__version__)
img = cv2.imread("image/bts-02.jpg")
print("image shape: {} pixels".format(img.shape))
print("width: {} pixels".format(img.shape[1]))
print("height: {} pixels".format(img.shape[0]))
print("channels: {}".format(img.shape[2]))
cv2.imshow("bts", img)
# b, g, r 순서 지키기
(b, g, r) = img[0, 0]
print("Pixel at (0, 0) - Red: {}, Green: {}, Blue: {}".format(r, g, b))
# 높이, 너비
img[100:150, 50:100] = (0, 0, 255)
#cv2.rectangle(img, (좌, 상), (우, 하), (b, g, r), 선굵기)
cv2.rectangle(img, (150, 100), (200, 150), (255, 0, 0), 5)
#cv2.circle(img, (좌, 상), 반지름, (b, g, r), 선굵기) # -1 모두 채움
cv2.circle(img, (275, 125), 25, (0, 255, 255), -1)
#cv2.line(img, (좌, 상), (우, 하), (b, g, r), 굵기)
cv2.line(img, (350, 100), (400, 150), (255, 0, 0), 5)
#cv2.putText(img, '문자열', 시작위치(좌, 상), font_style, 폰트크기, (b, g, r), 폰트굵기)
cv2.putText(img, 'Hello~ BTS', (450, 150), \
cv2.FONT_HERSHEY_SIMPLEX, 1, (255, 255, 255), 4)
cv2.imshow("bts - draw", img)
cv2.waitKey(0)
cv2.destroyAllWindows()