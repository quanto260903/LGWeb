'use client';

import React, { useState, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';
import '../app/css/abstracts/_variables.scss';
import '../app/css/components/_buttons.scss';
import '../app/css/pages/_about.scss';

import ceoimages from '../app/images/ceo.png';
import doubleflashtop from '../app/images/doubleflashtop.png';
import doubleflashbot from '../app/images/doubleflashbot.png';

import { BlogCategoryType } from '@/type/BlogCategory';
import { GetAboutCEO } from '@/api/about';

const AboutUs = ({ showButton = true }) => {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetAboutCEO();
      setData(result);
    };
    loadData();
  }, []);

  return (
    <>
      <section className="ceo-section">
        <div className="ceo-container">
          <div className="ceo-image">
            <Image
              src={ceoimages}
              alt="CEO Image"
              className="image"
              width={150}
              height={150}
              layout="intrinsic"
            />
          </div>
          
          {data && data.map((item, index)=> (
              <div key={index} className="ceo-details wrp">
                <h2 className="ceo-name tl3">Le Thanh Son</h2>
                <h3 className="ceo-title tl41">{item.name}</h3>
                <div className='double-flash-top'>
                  <Image
                    src={doubleflashtop}
                    alt="Image"
                    layout="intrinsic"
                  />
                </div>
                <p className="ceo-description tl4">
                  {item.description}
                </p>
                <p className="ceo-description tl4">
                  Add-On Development is an experienced Microsoft Gold Certified Partner and all relevant employees have one or more Microsoft certifications. Developing complex software tasks requires experienced employees and that is why Microsoft certifications play a key role in our company strategy. We are endeavoring to achieve excellence in designing, developing and producing software for the Microsoft Platform that adds value to any business. Furthermore, we are result-driven and we strongly believe in good teamwork, a streamlined process and innovative, and employee development, which all are key contributors to the final outcome.
                </p>
                <blockquote className="ceo-quote tl4">
                  {item.detail}
                  <div className='double-flash-bot'>
                    <Image
                      src={doubleflashbot}
                      alt="Image"
                      layout="intrinsic"
                    />
                  </div>
                </blockquote>
              </div>
            ))}

        </div>
      </section>
    </>
  );
};

export default AboutUs;
