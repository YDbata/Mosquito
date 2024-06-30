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
  Sequence1 --> EyeDetection;
  Sequence1 --> IsAttackRange1;
  Sequence1 --> Attack1;
  Sequence2 --> EarDetection;
  Sequence2 --> IsAttackRange2;
  Sequence2 --> Attack2;
  Sequence3 --> Hit;
  Sequence3 --> DetectEnemy;
  Sequence3 --> IsAttackRange3;
  Sequence3 --> Attack3;
  Selector --> BasicMotion;
```
