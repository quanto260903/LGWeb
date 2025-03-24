'use client';

import React, { useState, useEffect } from 'react';
import Link from 'next/link';

import '../app/css/abstracts/_variables.scss';
import '../app/css/components/_buttons.scss';
import '../app/css/pages/_about.scss';

import { BlogCategoryType } from '@/type/BlogCategory';
import { GetAboutTop } from '@/api/about';

const AboutUs = ({ showButton = true }) => {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetAboutTop();
      setData(result);
    };
    loadData();
  }, []);

  return (
    <section className="about-section">
      <h3 className="small-title tl">\ About us \</h3>
      <h1 className="main-title tl2">A shortcut to the big picture...</h1>

      <div className="about-container wrp">
        {data && data.filter(item => item.order === 1).map((item, index) => (
            <div key={index} className="about-partner">
              <div className='about-partner-image'>
                <img
                  src={item.imageUrl || '/picture/NoImage.png'}
                  style={{ width: '100%', height: 'auto' }}
                  alt={item.name || 'Image'}
                />
              </div>
              <div className="partner-details">
                <h2 className="partner-title tl3" style={{ color: item.color }}>{item.name}</h2>
                <p className="partner-description tl4">{item.description}</p>
              </div>
            </div>
          ))}

        {/* Values section wrapping all value-boxes */}
        <div className="about-values">
          {data&& data.filter(item => item.order != 1).map((item, index) => (
              <div key={index} className="value-box">
                <div className="value-icon">
                  <img
                    src={item.imageUrl || '/picture/NoImage.png'}
                    style={{ width: '100%', height: 'auto' }}
                    alt={item.name || 'Image'}
                  />
                </div>
                <h3 className="value-title tl3" style={{ color: item.color }}>{item.name}</h3>
                <div className='value-title-2'></div>
                <p className="tl4">{item.description}</p>
              </div>
            ))}
        </div>
      </div>

      {showButton && (
        <div className="button-container wrp">
          <Link href={'/about'}>
            <button className="read-more-btn">Read more</button>
          </Link>
        </div>
      )}
    </section>
  );
};

export default AboutUs;
