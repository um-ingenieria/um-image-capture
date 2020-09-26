UPDATE [dbo].[DEVO_ALL_SUBJECTS]
SET valence_mean = ABS(10-valence_mean),
[arousal_mean] = ABS(10-[arousal_mean]),
[impact_mean] = ABS(10-[impact_mean]),

[men_valence_mean] = ABS(10-[men_valence_mean]),
[men_arousal_mean] = ABS(10-[men_arousal_mean]),
[men_impact_mean] = ABS(10-[men_impact_mean]),

[women_valence_mean] = ABS(10-[women_valence_mean]),
[women_arousal_mean] = ABS(10-[women_arousal_mean]),
[women_impact_mean] = ABS(10-[women_impact_mean])

WHERE 1=1;