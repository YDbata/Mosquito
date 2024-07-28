# Mosquito
mosquito Game Dev


## BT
아래는 Human AI의 행동트리입니다.

```mermaid
graph TD;
  root --> Selector;
  Selector --> Sequence1;
  Selector --> Sequence2;
  Selector --> Sequence3;
  Sequence2 --> EyeDetection;
  Sequence2 --> Seek1;
  Sequence1 --> IsAttackRange;
  Sequence1 --> Attack;
  Sequence3 --> EarDetection;
  Sequence3 --> Seek2;
  Sequence4 --> Hit;
  Sequence4 --> LookAround;
  Sequence4 --> Seek;
  Selector --> Sequence4;
  Selector --> Sequence5;
  Sequence5 --> Invertor;
  Sequence5 --> BasicMotion;
  Invertor --> Detection;
```
