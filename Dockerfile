FROM golang:1.21-alpine AS builder
WORKDIR /app
COPY sorting-service/ .
RUN go mod download
RUN go build -o main .

FROM alpine:latest
WORKDIR /root/
COPY --from=builder /app/main .
EXPOSE 50051
CMD ["./main"]